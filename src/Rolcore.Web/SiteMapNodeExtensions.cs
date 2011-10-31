using System;
using System.Web;

namespace Rolcore.Web
{
    [Obsolete("If your site is big enough to use SiteMaps, you should be using HereSay.")]
    public static class SiteMapNodeExtensions
    {
        /// <summary>
        /// Determines how many levels deep a given <see cref="SiteMapNode"/> is from the
        /// root (zero based).
        /// </summary>
        /// <param name="node">The node to determine the depth of.</param>
        /// <returns>An <see cref="int"/> representing the depth of the given node.</returns>
        [Obsolete("If your site is big enough to use SiteMaps, you should be using HereSay.")]
        public static int GetDepthLevel(this SiteMapNode node)
        {   //TODO: Unit test
            // Pre-conditions
            if (node == null)
                throw new ArgumentNullException("node", "node is null.");

            SiteMapNode level1Node = node.GetParentSiteMapNodeByLevel(1); // First non-root 
            int result = 0;                                               // parent.
            SiteMapNode currentNode = node;
            while ((currentNode.ParentNode != null) && (currentNode != level1Node)) // Count the distance to the first non-root
            {                                                                       // parent.
                result++;
                currentNode = currentNode.ParentNode;
            }
            return ++result; // increment by one because we're zero-based
        }

        /// <summary>
        /// Gets the parent <see cref="SiteMapNode"/> that occurs at a specified depth 
        /// within the <see cref="SiteMap"/> structure (from the root, which is at level 
        /// zero).
        /// </summary>
        /// <param name="childNode">The node to get the partent <see cref="SiteMapNode"/>
        /// of.</param>
        /// <param name="level">The zero-based level of the desired parent node.</param>
        /// <returns>A <see cref="SiteMapNode"/>.</returns>
        [Obsolete("If your site is big enough to use SiteMaps, you should be using HereSay.")]
        public static SiteMapNode GetParentSiteMapNodeByLevel(this SiteMapNode childNode, int level)
        {
            //TODO: Unit test
            // Pre-conditions
            if (level < 0)
                throw new ArgumentOutOfRangeException("level", "level cannot be negative");
            if (childNode == null)
                return null;

            const int rootLevel = 0;
            if (level == rootLevel)
                return childNode.RootNode;

            if (level == 1) // Short-cut for first non-root parent
            {
                if (childNode == SiteMap.RootNode)
                    return null;
                SiteMapNode result = childNode;

                while ((result.ParentNode != null) && (result.ParentNode != childNode.RootNode))
                    result = result.ParentNode;
                return result;
            }
            int childNodeLevel = childNode.GetDepthLevel();
            SiteMapNode currentLevelNode = childNode;
            while (childNodeLevel > level)
            {
                currentLevelNode = currentLevelNode.ParentNode;
                childNodeLevel--;
            }

            return currentLevelNode;
        }
    }
}
