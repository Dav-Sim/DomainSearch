using DomainSearch.Models;
using DomainSearch.Services;
using DomainSearch.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DomainSearch.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// main viev model
        /// </summary>
        private readonly MainVM _Model;
        /// <summary>
        /// Searcher class for getting whois infos 
        /// </summary>
        private readonly Searcher _Searcher;
        public MainWindow()
        {
            InitializeComponent();
            _Model = this.DataContext as MainVM;
            _Searcher = new Searcher();
        }

        private void CommandStart_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == true)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private async void CommandStart_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Progress<ReportProgress> progress = new Progress<ReportProgress>();
            progress.ProgressChanged += Progress_ProgressChanged;
            _Model.Working = true;
            _Model.Sites.Clear();
            try
            {
                await _Searcher.SearchAsync(_Model.Domains, _Model.Inputs, progress);
            }
            catch (Exception ex) when (ex is OperationCanceledException || ex is ObjectDisposedException)
            {
                _Model.Progress = "Cancelled";
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                progress.ProgressChanged -= Progress_ProgressChanged;
                _Model.Working = false;
                _Model.Percent = 0;
                _Model.Progress = "0%";
            }
        }

        private async void Progress_ProgressChanged(object sender, ReportProgress e)
        {
            if (!Dispatcher.CheckAccess())
            {
                await Dispatcher.BeginInvoke((Action)(() =>
                {
                    UpdateProgress(e.Percent, e.Progress, e.SiteReady);
                }));
            }
            else
            {
                UpdateProgress(e.Percent, e.Progress, e.SiteReady);
            }
        }

        private void UpdateProgress(int percent, string text, SiteInfo info)
        {
            _Model.Percent = percent;
            _Model.Progress = text;
            if (info != null)
            {
                _Model.Sites.Add(info);
            }
        }

        private void CommandStop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _Searcher.Stop();
        }

        private void CommandCopy_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == false && _Model?.Sites?.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == false && _Model?.Sites?.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == false && _Model?.Sites?.Count > 0)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandCopy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Model?.Sites?.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Models.SiteInfo.GetHeading());
                foreach (var site in _Model.Sites)
                {
                    sb.AppendLine(site.ToSeparatedValuesString());
                }
                Clipboard.SetText(sb.ToString());
            }
        }

        private void CommandDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _Model?.Sites?.Clear();
        }

        private void CommandSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Model?.Sites?.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Models.SiteInfo.GetHeading());
                foreach (var site in _Model.Sites)
                {
                    sb.AppendLine(site.ToSeparatedValuesString());
                }
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "csv files|*.csv";
                dialog.DefaultExt = "csv";
                dialog.Title = "Save domain search results";
                dialog.ValidateNames = true;
                dialog.FileName = $"Domain_Search_Results_{DateTime.Now:yyyy_MM_dd-HH.mm.ss}.csv";
                if (dialog.ShowDialog(this) == true)
                {
                    System.IO.File.WriteAllText(dialog.FileName, sb.ToString());
                }
            }
        }

        private void CommandPauseResume_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (_Model?.Working == true)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void CommandPauseResume_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Model != null && _Searcher != null)
            {
                _Model.Paused = _Searcher.PauseResume();
            }
        }
    }
}
