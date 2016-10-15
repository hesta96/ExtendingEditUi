using System.Configuration.Provider;
using System.Web.Security;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using log4net;

namespace ExtendingEditUi.Business
{
    [InitializableModule]
    public class CreateAdminUserAndRoles : IInitializableModule
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CreateAdminUserAndRoles));

        public void Initialize(InitializationEngine context)
        {
#if DEBUG
            var mu = Membership.GetUser("EpiSQLAdmin");

            if (mu != null) return;

            try
            {
                Membership.CreateUser("EpiSQLAdmin", "6hEthU", "EpiSQLAdmin@site.com");

                try
                {
                    this.EnsureRoleExists("WebEditors");
                    this.EnsureRoleExists("WebAdmins");

                    Roles.AddUserToRoles("EpiSQLAdmin", new[] { "WebAdmins", "WebEditors" });
                }
                catch (ProviderException pe)
                {
                    Log.Error(pe);
                }
            }
            catch (MembershipCreateUserException mcue)
            {
                Log.Error(mcue);
            }
#endif
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        private void EnsureRoleExists(string roleName)
        {
            if (Roles.RoleExists(roleName)) return;

            try
            {
                Roles.CreateRole(roleName);
            }
            catch (ProviderException pe)
            {
                Log.Error(pe);
            }
        }
    }
}