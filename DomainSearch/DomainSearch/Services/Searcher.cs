using DomainSearch.Models;
using DomainSearch.ViewModels;
using Microsoft.VisualStudio.Threading;
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
        private AsyncManualResetEvent _ResetEvent;

        /// <summary>
        /// Search for domains and report progress
        /// </summary>
        /// <param name="domains">collection of domains</param>
        /// <param name="inputs">collection of input strings</param>
        /// <param name="progress">progress object</param>
        /// <returns>collection of whois sites informations</returns>
        public async Task<List<SiteInfo>> SearchAsync(IEnumerable<Domain> domains, IEnumerable<string> inputs, IProgress<ReportProgress> progress)
        {
            _Token = new CancellationTokenSource();
            _ResetEvent = new AsyncManualResetEvent(true);
            List<SiteInfo> sites = new List<SiteInfo>();
            var addresses = inputs.SelectMany(input => domains.Where(dom => dom.IsChecked).Select(dom => input + dom.Name)).ToList();
            int total = addresses.Count;
            try
            {
                using (var whois = new WhoisLookup())
                {
                    for (int i = 0; i < addresses.Count; i++)
                    {
                        //cancellation
                        _Token.Token.ThrowIfCancellationRequested();
                        //pause
                        await _ResetEvent.WaitAsync();

                        SiteInfo site;
                        var adr = addresses[i];
                        try
                        {
                            WhoisResponse response = await whois.LookupAsync(adr);
                            site = new SiteInfo(adr, response);
                            sites.Add(site);
                        }
                        catch (Whois.WhoisException)
                        {
                            site = SiteInfo.Error(adr);
                            sites.Add(site);
                        }
                        catch (System.TimeoutException)
                        {
                            site = SiteInfo.Error(adr);
                            sites.Add(site);
                        }
                        catch (Exception)
                        {
                            throw;
                        }

                        int done = (int)((i + 1.0) / total * 100.0);
                        progress.Report(new ReportProgress(done, $"Done {i + 1} of {total}", site));
                    }
                }
                return sites;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// stops current search
        /// </summary>
        public void Stop()
        {
            _Token?.Cancel();
        }

        /// <summary>
        /// pause current search or resume depending on current state
        /// </summary>
        /// <returns>returns state true for paused, false for resumed</returns>
        public bool PauseResume()
        {
            if (_ResetEvent != null)
            {
                if (_ResetEvent.IsSet)
                {
                    _ResetEvent?.Reset();
                    return true;
                }
                else
                {
                    _ResetEvent.Set();
                    return false;
                }
            }
            throw new InvalidOperationException();
        }
    }
}
