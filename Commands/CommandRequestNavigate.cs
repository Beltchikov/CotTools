using System;
using System.Diagnostics;
using System.Windows.Input;

namespace CotTools.Commands
{
    /// <summary>
    /// CommandRequestNavigate
    /// </summary>
    public class CommandRequestNavigate : ICommand
    {
        /// <summary>
        /// CanExecuteChanged
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// CanExecute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Process.Start(new ProcessStartInfo(((Uri)parameter).AbsoluteUri) { UseShellExecute = true });
        }
    }
}
