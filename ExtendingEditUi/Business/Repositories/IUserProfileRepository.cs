using System.Collections.Generic;
using ExtendingEditUi.Models.Entities;

namespace ExtendingEditUi.Business.Repositories
{
    public interface IUserProfileRepository
    {
        IEnumerable<UserProfile> SearchUsers(string searchString);
        UserProfile GetUserProfile(string userId);
        UserProfile UpsertUserProfile(UserProfile userProfile);
    }
}