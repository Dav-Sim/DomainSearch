using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSearch.Models
{
    /// <summary>
    /// class for reporting progress
    /// </summary>
    public class ReportProgress
    {
        public ReportProgress(int percent, string progress, SiteInfo siteReady)
        {
            Percent = percent;
            Progress = progress;
            SiteReady = siteReady;
        }

        /// <summary>
        /// percent done
        /// </summary>
        public int Percent { get; }
        /// <summary>
        /// progress text
        /// </summary>
        public string Progress { get; }
        /// <summary>
        /// completed site info
        /// </summary>
        public SiteInfo SiteReady { get; }
    }
}
