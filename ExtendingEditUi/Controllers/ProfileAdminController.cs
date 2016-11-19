using System.Web.Mvc;

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
            return View();
        }
    }
}