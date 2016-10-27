using System.Web.Mvc;
using ExtendingEditUi.Models.ViewModels;

namespace ExtendingEditUi.Controllers
{
    [Authorize(Roles = "Administrators, WebAdmins, WebEditors")]
    public class ProfileAdminController : Controller
    {
        // GET: ProfileAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profiles()
        {
            var model = new ProfilesViewModel();

            return View(model);
        }
    }
}