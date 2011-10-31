using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;

namespace Rolcore.Web.Syndication
{
    public static class SyndicationFeedExtensions
    {
        public static SyndicationFeed Reduce(this SyndicationFeed feed, int maxArticles)
        {
            //
            // Pre-conditions

            if (feed == null)
                throw new ArgumentNullException("feed");
            if (maxArticles < 1)
                throw new ArgumentOutOfRangeException("maxArticles", "Cannot reduce to less than one article.");
            
            //
            // Do we actually need to do anything?

            if (feed.Items.Count() <= maxArticles)
                return feed;

            //
            // Reduce the feed size

            SyndicationItem[] feedItems = feed.Items.Take(maxArticles).ToArray();
            Uri altLink = feed.Links
                .Where(link => link.RelationshipType == "alternate")
                .First()
                .GetAbsoluteUri();

            SyndicationFeed result = new SyndicationFeed(
                feed.Title.Text,
                feed.Description.Text,
                altLink,
                feed.Id,
                feed.LastUpdatedTime,
                feedItems);
            result.BaseUri = feed.BaseUri;

            return result;
        }
    }
}
