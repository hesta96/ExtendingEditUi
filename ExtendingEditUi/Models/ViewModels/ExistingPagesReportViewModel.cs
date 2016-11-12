using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.DataAbstraction;

namespace ExtendingEditUi.Models.ViewModels
{
    public class ExistingPagesReportViewModel
    {
        public IEnumerable<PageType> PageTypes { get; set; }
        public PageDataCollection Pages { get; set; }
        public string SelectedPageType { get; set; }
    }
}