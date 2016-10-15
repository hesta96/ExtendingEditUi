using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult Profies()
        {
            var model = new ProfilesViewModel();

            return View(model);
        }
    }
}