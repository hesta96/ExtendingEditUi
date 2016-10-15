using EPiServer.Core;

namespace ExtendingEditUi.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
