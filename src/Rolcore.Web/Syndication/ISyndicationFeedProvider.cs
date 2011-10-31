using System.ServiceModel.Syndication;

namespace Rolcore.Web.Syndication
{
    /// <summary>
    /// When implemented, provides <see cref="SyndicationFeed"/> access.
    /// </summary>
    public interface ISyndicationFeedProvider
    {
        /// <summary>
        /// Gets a <see cref="SyndicationFeed"/> with full article content.
        /// </summary>
        SyndicationFeed FullFeed { get; }
        /// <summary>
        /// Gets a <see cref="SyndicationFeed"/> with partial article content.
        /// </summary>
        SyndicationFeed PartialFeed { get; }
    }
}
