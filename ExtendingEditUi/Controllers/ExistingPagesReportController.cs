using System;
using System.Web;
using System.Web.Mvc;
using EPiServer.Core;
using EPiServer.Web.Mvc.Html;
using ExtendingEditUi.Helpers;
using ExtendingEditUi.Models.ViewModels;
using OfficeOpenXml;

namespace ExtendingEditUi.Controllers
{
    [EPiServer.PlugIn.GuiPlugIn(
        Area = EPiServer.PlugIn.PlugInArea.ReportMenu, 
        Url = "~/existingpagesreport",
        Category = "Existing Pages",
        DisplayName = "Pages By PageType")]
    [Authorize(Roles = "Administrators, WebAdmins")]
    public class ExistingPagesReportController : Controller
    {
        public ActionResult Index()
        {
            var model = new ExistingPagesReportViewModel { PageTypes = ExistingPagesHelper.GetAllPageTypes() };

            return View(model);
        }

        [HttpPost]
        public ActionResult ListPages(FormCollection form)
        {
            var model = new ExistingPagesReportViewModel
            {
                PageTypes = ExistingPagesHelper.GetAllPageTypes(),
                SelectedPageType = form["pageType"]
            };

            ExistingPagesHelper.SetPagesForPageTypeName(model);


            var doExport = false;

            if (bool.TryParse(form["doExport"], out doExport) && doExport && model.Pages != null && model.Pages.Count > 0)
            {
                Export(model.Pages, System.Web.HttpContext.Current.Response);
            }

            return View("Index", model);
        }

        public void Export(PageDataCollection pagesToExport, HttpResponse response)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.Add("pages");

                ws.Cells[1, 1].Value = "PageId";
                ws.Cells[1, 2].Value = "PageName";
                ws.Cells[1, 3].Value = "PageUrl";
                ws.Cells[1, 4].Value = "Published Date";

                ws.Row(1).Style.Font.Bold = true;
                ws.Row(1).Style.Locked = true;

                int row = 2;

                foreach (var page in pagesToExport)
                {
                    ws.Cells[row, 1].Value = page.ContentLink.ID;
                    ws.Cells[row, 2].Value = page.PageName;
                    ws.Cells[row, 3].Value = Url.ContentUrl(page.ContentLink);
                    ws.Cells[row, 4].Value = page.StartPublish.HasValue ? page.StartPublish.Value.ToString("yyyy-MM-dd HH:mm") : "Not published";

                    ++row;
                }

                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", string.Format("attachment; filename=pages{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")));
                response.BinaryWrite(package.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }
    }
}