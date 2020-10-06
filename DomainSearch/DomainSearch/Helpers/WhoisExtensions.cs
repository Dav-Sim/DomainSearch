using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainSearch.Helpers
{
    /// <summary>
    /// extension methods for whois
    /// </summary>
    public static class WhoisExtensions
    {
        /// <summary>
        /// converts contact to readable string
        /// </summary>
        public static string ToWhoisString(this Whois.Models.Contact contact)
        {
            if (contact == null)
            {
                return "";
            }
            return $"{contact.Name} {contact.Email} {contact.Organization}".Trim();
        }
        /// <summary>
        /// converts registrar to readable string
        /// </summary>
        public static string ToWhoisString(this Whois.Models.Registrar registrar)
        {
            if (registrar == null)
            {
                return "";
            }
            return $"{registrar.Name} {registrar.AbuseEmail} {registrar.AbuseTelephoneNumber} {registrar.IanaId} {registrar.Url}".Trim();
        }
        /// <summary>
        /// converts trademark to readable string
        /// </summary>
        public static string ToWhoisString(this Whois.Models.Trademark trademark)
        {
            if (trademark == null)
            {
                return "";
            }
            return $"{trademark.Name} {trademark.Number} {trademark.Date:MM-yyyy} {trademark.Country}".Trim();
        }
        /// <summary>
        /// converts hostname to readable string
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static string ToWhoisString(this Whois.HostName host)
        {
            if (host == null)
            {
                return "";
            }
            return $"{host.Value}".Trim();
        }

    }
}
