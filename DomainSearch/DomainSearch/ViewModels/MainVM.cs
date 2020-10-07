using DomainSearch.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DomainSearch.ViewModels
{
    /// <summary>
    /// main page view model 
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        private string inputText = "test";
        private int percent = 0;
        private string progress = "0%";
        private bool working = false;
        private bool paused = false;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// collection of tol level domains
        /// </summary>
        public ObservableCollection<Domain> Domains { get; set; } = new ObservableCollection<Domain>()
        {
            new Domain(".COM", true),
            new Domain(".NET", true),
            new Domain(".EU", true),
            new Domain(".CZ", true),
            new Domain(".INFO", true),
            new Domain(".BIZ", false),
            new Domain(".NAME", false),
            new Domain(".ORG", false)
        };

        /// <summary>
        /// collection of input strings (to pair with top level domains)
        /// </summary>
        public ObservableCollection<string> Inputs { get; set; } = new ObservableCollection<string>()
        {
            "test"
        };

        public string InputText
        {
            get { return inputText; }
            set { inputText = value; Notify(nameof(InputText)); ChangeInputs(value); }
        }

        public int Percent
        {
            get { return percent; }
            set { percent = value; Notify(nameof(Percent)); }
        }

        public string Progress
        {
            get { return progress; }
            set { progress = value; Notify(nameof(Progress)); }
        }

        /// <summary>
        /// collection of result whois sites informations
        /// </summary>
        public ObservableCollection<SiteInfo> Sites { get; set; } = new ObservableCollection<SiteInfo>();

        public bool Working
        {
            get { return working; }
            set { working = value; Notify(nameof(Working)); CommandManager.InvalidateRequerySuggested(); }
        }

        public bool Paused
        {
            get { return paused; }
            set { paused = value; Notify(nameof(Paused)); }
        }

        private void ChangeInputs(string value)
        {
            Inputs.Clear();
            foreach (var inp in value.Split(new string[] {",", " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                Inputs.Add(inp.ToUpper());
            }
        }

        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
