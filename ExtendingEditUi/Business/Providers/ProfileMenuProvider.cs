using System.Collections.Generic;
using EPiServer.Security;
using EPiServer.Shell.Navigation;

namespace ExtendingEditUi.Business.Providers
{
    [MenuProvider]
    public class ProfileMenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var toolbox = new SectionMenuItem("Profile-Admin", "/global/profileadmin")
            {
                IsAvailable = (request) => PrincipalInfo.HasEditorAccess
            };

            var profies = new UrlMenuItem("User profiles", "/global/profileadmin/profiles", "/profileadmin/profiles")
            {
                IsAvailable = (request) => PrincipalInfo.HasEditorAccess
            };

            return new MenuItem[] { toolbox, profies };
        }
    }
}