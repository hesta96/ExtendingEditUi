using System.ComponentModel.DataAnnotations;
using EPiServer.Filters;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.Web.Mvc.VisitorGroups;

namespace ExtendingEditUi.Models.VisitorGroupCriterion
{
    public class BrowserModel : CriterionModelBase
    {
        [DojoWidget(
        SelectionFactoryType = typeof(EnumSelectionFactory),
        LabelTranslationKey = "/shell/cms/visitorgroups/criteria/browser/browsertype",
        AdditionalOptions = "{ selectOnClick: true }"),
        Required]
        public BrowserType Browser { get; set; }

        [DojoWidget(
            SelectionFactoryType = typeof(EnumSelectionFactory),
            LabelTranslationKey = "/shell/cms/visitorgroups/criteria/browser/comparecondition",
            AdditionalOptions = "{ selectOnClick: true }"),
            Required]
        public CompareCondition Condition { get; set; }
        [DojoWidget(
            DefaultValue = 0,
            LabelTranslationKey = "/shell/cms/visitorgroups/criteria/browser/majorversion",
            AdditionalOptions = "{ constraints: {min: 0}, selectOnClick: true }"),
            Range(0, 0xff)]
        public int MajorVersion { get; set; }

        public override ICriterionModel Copy()
        {
            return ShallowCopy();
        }
    }

    public enum BrowserType
    {
        IE,
        FireFox,
        Chrome,
        Other
    }
}