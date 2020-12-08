using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace CotTools.Commands
{
    public class CommandRequestNavigate : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Process.Start(new ProcessStartInfo(((System.Uri)parameter).AbsoluteUri) { UseShellExecute = true });
        }
    }
}
