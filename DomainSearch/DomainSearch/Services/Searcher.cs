using DomainSearch.Models;
using DomainSearch.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Whois;

namespace DomainSearch.Services
{
    /// <summary>
    /// whois searcher class
    /// </summary>
    public class Searcher
    {
        private CancellationTokenSource _Token;

        /// <summary>
        /// Search for domains and report progress
        /// </summary>
        /// <param name="domains">collection of domains</param>
        /// <param name="inputs">collection of input strings</param>
        /// <param name="progress">progress object</param>
        /// <returns>collection of whois sites informations</returns>
        public async Task<List<SiteInfo>> Search(IEnumerable<Domain> domains, IEnumerable<string> inputs, IProgress<ReportProgress> progress)
        {
            var whois = new WhoisLookup();
            _Token = new CancellationTokenSource();
            int repeat = 0;
            int maxRepeat = 2;
            List<string> combinations = new List<string>();
            try
            {
                List<SiteInfo> sites = new List<SiteInfo>();
                int total = inputs.Count() * domains.Count(a => a.IsChecked);

                foreach (var input in inputs)
                {
                    foreach (var dom in domains)
                    {
                        if (dom.IsChecked)
                        {
                            combinations.Add(input + dom.Name);
                        }
                    }
                }
                for (int i = 0; i < combinations.Count; i++)
                {
                    //cancellation
                    _Token.Token.ThrowIfCancellationRequested();

                    var adr = combinations[i];
                    try
                    {
                        var response = await whois.LookupAsync(adr);
                        var site = new SiteInfo(adr, response);
                        sites.Add(site);

                        int done = (int)(((double)(i+1) / (double)total) * 100.0);
                        progress.Report(new ReportProgress(done, $"Done {i+1} of {total}", site));
                        repeat = 0;
                    }
                    catch (Whois.WhoisException)
                    {
                        if (repeat < maxRepeat)
                        {
                            repeat++;
                            i--;
                            whois.Dispose();
                            whois = new WhoisLookup();
                            continue;
                        }
                        else
                        {
                            int done = (int)(((double)(i + 1) / (double)total) * 100.0);
                            progress.Report(new ReportProgress(done, $"Done {i + 1} of {total}", SiteInfo.Error(adr)));
                            repeat = 0;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return sites;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                whois.Dispose();
            }
        }

        /// <summary>
        /// stops current search
        /// </summary>
        public void Stop()
        {
            _Token?.Cancel();
        }
    }
}
