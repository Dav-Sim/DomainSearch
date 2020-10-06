namespace DomainSearch.Models
{
    /// <summary>
    /// class represents top level domain (TLD)
    /// </summary>
    public class Domain
    {
        public Domain(string name, bool isChecked)
        {
            Name = name;
            IsChecked = isChecked;
        }

        /// <summary>
        /// name of TLD (top level domain) including leading dot
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// selected for search
        /// </summary>
        public bool IsChecked { get; set; }
    }
}
