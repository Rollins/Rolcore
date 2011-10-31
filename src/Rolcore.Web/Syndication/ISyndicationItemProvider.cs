using System.ServiceModel.Syndication;

namespace Rolcore.Web.Syndication
{
    public interface ISyndicationItemProvider
    {
        SyndicationItem FullItem { get; }
        SyndicationItem PartialItem { get; }
    }
}
