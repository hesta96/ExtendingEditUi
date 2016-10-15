using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ExtendingEditUi.Models.Entities;

namespace ExtendingEditUi.Business.Repositories
{
    public interface IUserProfileRepository
    {
        IEnumerable<UserProfile> SearchUsers(string searchString);
        UserProfile GetUserProfile(string userId);
        UserProfile UpsertUserProfile(UserProfile userProfile);
    }
    
    public class UserProfileRepository : IUserProfileRepository
    {
        public IEnumerable<UserProfile> SearchUsers(string searchString)
        {
            const string Query = @"Select u.UserId, u.UserName, p.PropertyValueStrings as Email, up.FirstName, up.LastName, up.PhoneNumber
                                    From Users u 
	                                    Inner join Profiles p on (u.UserId = p.UserId AND p.PropertyNames like 'Email:%')
	                                    Left outer join UserProfile up on u.UserId = up.UserId
                                    Where UserName like @searchString OR (ISNULL(up.FirstName, '') + ' ' + ISNULL(up.LastName, '')) like @searchString";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString))
            {
                conn.Open();
                return conn.Query<UserProfile>(Query, new { SearchString = string.Format("%{0}%", searchString)});
            }
        }

        public UserProfile GetUserProfile(string userId)
        {
            const string Query = @"Select u.UserId, u.UserName, p.PropertyValueStrings as Email, up.FirstName, up.LastName, up.PhoneNumber
                                    From Users u 
	                                    Inner join Profiles p on (u.UserId = p.UserId AND p.PropertyNames like 'Email:%')
	                                    Left outer join UserProfile up on u.UserId = up.UserId
                                    Where u.UserId = @UserId";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString))
            {
                conn.Open();
                return conn.Query<UserProfile>(Query, new { UserId = userId }).SingleOrDefault();
            }
        }

        public UserProfile UpsertUserProfile(UserProfile userProfile)
        {
            const string Query = @"Update UserProfile
                                    Set FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber
                                    Where UserId = @UserId

                                    If @@ROWCOUNT = 0
                                    BEGIN
	
	                                    Insert Into UserProfile
	                                    (UserId, FirstName, LastName, PhoneNumber)
	                                    Values
	                                    (@UserId, @FirstName, @LastName, @PhoneNumber)
                                    END

                                    Select u.UserId, u.UserName, p.PropertyValueStrings as Email, up.FirstName, up.LastName, up.PhoneNumber
                                    From Users u 
	                                    Inner join Profiles p on (u.UserId = p.UserId AND p.PropertyNames like 'Email:%')
	                                    Left outer join UserProfile up on u.UserId = up.UserId
                                    Where u.UserId = @UserId";

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["EPiServerDB"].ConnectionString))
            {
                conn.Open();
                return conn.Query<UserProfile>(Query, new {userProfile.UserId, userProfile.FirstName, userProfile.LastName, userProfile.PhoneNumber }).SingleOrDefault();
            }
        }
    }
}