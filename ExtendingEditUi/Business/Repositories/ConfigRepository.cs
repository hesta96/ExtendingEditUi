using System.Configuration;

namespace ExtendingEditUi.Business.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        public string GetConnectionString(string key)
        {
            var conString = ConfigurationManager.ConnectionStrings[key];
            if (conString != null)
            {
                return conString.ConnectionString;
            }

            return string.Empty;
        }
    }
}