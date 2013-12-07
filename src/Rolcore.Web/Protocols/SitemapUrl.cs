using System;
using System.Diagnostics;

namespace Rolcore.Web.Protocols
{
    [DebuggerDisplay("{Loc}")]
    public class SitemapUrl
    {
        private double? _RelativePriority;

        public double? RelativePriority
        {
            get
            {
                return _RelativePriority;
            }
            set
            {
                if (value.HasValue)
                {
                    if (value.Value > 1 || value.Value < 0)
                        throw new ArgumentOutOfRangeException("RelativePriority must be between 0 and 1 (inclusive).");
                }

                _RelativePriority = value;
            }
        }
        public string Loc { get; set; }
        public SitemapChangeFreq? ChangeFrequency { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string LastMod
        {
            get
            {
                return this.LastModifiedDate.HasValue ? this.LastModifiedDate.Value.ToString("yyyy-MM-dd") : null;
            }
        }

        public string ChangeFreq
        {
            get
            {
                return this.ChangeFrequency.HasValue 
                    ? Enum.GetName(this.ChangeFrequency.GetType(), this.ChangeFrequency).ToLower()
                    : null;
            }
        }

        public string Priority
        {
            get
            {
                return this.RelativePriority.HasValue ? this.RelativePriority.Value.ToString("F3") : null;
            }
        }

    }
}
