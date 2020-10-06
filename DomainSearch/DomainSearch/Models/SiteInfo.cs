using DomainSearch.Helpers;
using System;

namespace DomainSearch.Models
{
    /// <summary>
    /// class for storing whois information about site
    /// </summary>
    public class SiteInfo
    {
        public SiteInfo(string name)
        {
            Name = name;
        }

        public SiteInfo(string name, Whois.WhoisResponse response)
        {
            Name = name;
            TemplateName = response.TemplateName;
            DnsSecStatus = response.DnsSecStatus;
            Remarks = response.Remarks;
            AdminContact = response.AdminContact.ToWhoisString();
            TechnicalContact = response.TechnicalContact.ToWhoisString();
            Registrar = response.Registrar.ToWhoisString();
            Expiration = response.Expiration;
            Updated = response.Updated;
            Registered = response.Registered;
            RegistryDomainId = response.RegistryDomainId;
            DomainName = response.DomainName.ToWhoisString();
            Status = response.Status.ToString();
            ContentLength = response.ContentLength;
            Registrant = response.Registrant.ToWhoisString();
            WhoisServerUrl = response.WhoisServerUrl;
        }

        public string Name { get; }
        public string TemplateName { get; }
        public string DnsSecStatus { get; }
        public string Remarks { get; }
        public string AdminContact { get; }
        public string TechnicalContact { get; }
        public string Registrar { get; }
        public DateTime? Expiration { get; }
        public DateTime? Updated { get; }
        public DateTime? Registered { get; }
        public string RegistryDomainId { get; }
        public string DomainName { get; }
        public string Status { get; private set; }
        public int ContentLength { get; }
        public string Registrant { get; }
        public string WhoisServerUrl { get; }
        public bool IsFree { get { return Status == "NotFound"; } }
        public bool HasError { get; private set; }

        public override string ToString()
        {
            return $"IsFree='{IsFree}' Name='{Name}' Status='{Status}' Expiration='{Expiration}' " +
                $"Registrant='{Registrant}' AdminContact='{AdminContact}' TechnicalContact='{TechnicalContact}' " +
                $"TemplateName='{TemplateName}' DnsSecStatus='{DnsSecStatus}' Registrar='{Registrar}' Updated='{Updated}' " +
                $"Registered='{Registered}' RegistryDomainId='{RegistryDomainId}' DomainName='{DomainName}' " +
                $"WhoisServerUrl='{WhoisServerUrl}' Remarks='{Remarks}'";
        }

        /// <summary>
        /// returns values separated by delimiter
        /// </summary>
        /// <param name="separator">any string to place between values</param>
        /// <returns>values separated by delimiter</returns>
        public string ToSeparatedValuesString(string separator = ";")
        {
            return $"{IsFree}{separator}{Name}{separator}{Status}{separator}{Expiration}{separator}{Registrant}{separator}" +
                $"{AdminContact}{separator}{TechnicalContact}{separator}{TemplateName}{separator}{DnsSecStatus}{separator}" +
                $"{Registrar}{separator}{Updated}{separator}{Registered}{separator}{RegistryDomainId}{separator}{DomainName}{separator}" +
                $"{WhoisServerUrl}{separator}{Remarks}";
        }

        /// <summary>
        /// returns headings separated by delimiter
        /// </summary>
        /// <param name="separator">any string to place between headings</param>
        /// <returns>headings separated by delimiter</returns>
        public static string GetHeading(string separator = ";")
        {
            return $"IsFree{separator}Name{separator}Status{separator}Expiration{separator}Registrant{separator}AdminContact{separator}" +
                $"TechnicalContact{separator}TemplateName{separator}DnsSecStatus{separator}Registrar{separator}Updated{separator}" +
                $"Registered{separator}RegistryDomainId{separator}DomainName{separator}WhoisServerUrl{separator}Remarks";
        }

        /// <summary>
        /// Returns new instance of siteinfo with error flag set to true 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SiteInfo Error(string name)
        {
            return new SiteInfo(name) { HasError = true, Status = "ERROR please try again later..." };
        }
    }
}
