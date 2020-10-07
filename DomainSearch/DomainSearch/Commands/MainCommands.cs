using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DomainSearch.Commands
{
    /// <summary>
    /// main window commands
    /// </summary>
    public static class MainCommands
    {
        public static RoutedUICommand StartCommand = new RoutedUICommand("Start", nameof(StartCommand), typeof(MainCommands));
        public static RoutedUICommand StopCommand = new RoutedUICommand("Stop", nameof(StopCommand), typeof(MainCommands));
        public static RoutedUICommand PauseResumeCommand = new RoutedUICommand("PauseResume", nameof(PauseResumeCommand), typeof(MainCommands));
    }
}
