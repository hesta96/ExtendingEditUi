using System.Collections.Generic;
using System.Linq;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Filters;
using EPiServer.ServiceLocation;
using ExtendingEditUi.Models.ViewModels;

namespace ExtendingEditUi.Helpers
{
    public static class ExistingPagesHelper
    {
        public static IEnumerable<PageType> GetAllPageTypes()
        {
            var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            return contentTypeRepository.List().OfType<PageType>();
        }

        public static void SetPagesForPageTypeName(ExistingPagesReportViewModel model)
        {
            var criterias = new PropertyCriteriaCollection();

            var criteria = new PropertyCriteria();
            criteria.Condition = CompareCondition.Equal;
            criteria.Name = "PageTypeID";
            criteria.Type = PropertyDataType.PageType;
            criteria.Value = model.SelectedPageType;
            criteria.Required = true;

            criterias.Add(criteria);

            var pages = DataFactory.Instance.FindPagesWithCriteria(ContentReference.RootPage, criterias);

            model.Pages = pages;
        }
    }
}