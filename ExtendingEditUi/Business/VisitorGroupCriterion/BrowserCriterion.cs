using System;
using System.Security.Principal;
using System.Web;
using EPiServer.Filters;
using EPiServer.Personalization.VisitorGroups;
using ExtendingEditUi.Models.VisitorGroupCriterion;

namespace ExtendingEditUi.Business.VisitorGroupCriterion
{
    [VisitorGroupCriterion(
        Category = "User Criteria",
        DisplayName = "Browser",
        Description = "Criterion that matches type and version of the user's browser",
        LanguagePath = "/shell/cms/visitorgroups/criteria/browser")]
    public class BrowserCriterion : CriterionBase<BrowserModel>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            return MatchBrowserType(httpContext.Request.Browser.Browser) &&
                   MatchBrowserVersion(httpContext.Request.Browser.MajorVersion);
        }

        protected virtual bool MatchBrowserVersion(int majorVersion)
        {
            switch (Model.Condition)
            {
                case CompareCondition.LessThan:
                    return majorVersion < Model.MajorVersion;
                case CompareCondition.Equal:
                    return majorVersion == Model.MajorVersion;
                case CompareCondition.GreaterThan:
                    return majorVersion > Model.MajorVersion;
                case CompareCondition.NotEqual:
                    return majorVersion != Model.MajorVersion;
                default:
                    return false;
            }
        }

        protected virtual bool MatchBrowserType(string browserType)
        {
            browserType = (browserType ?? String.Empty).ToLower();
            if (browserType.Equals("ie") || browserType.Equals("internetexplorer"))
            {
                return Model.Browser == BrowserType.IE;
            }
            if (browserType.Equals("firefox"))
            {
                return Model.Browser == BrowserType.FireFox;
            }

            if (browserType.Equals("chrome"))
            {
                return Model.Browser == BrowserType.Chrome;
            }

            return Model.Browser == BrowserType.Other;
        }
    }
}