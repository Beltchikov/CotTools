using System;
using System.Diagnostics;
using System.Windows.Input;

namespace CotTools.Commands
{
    /// <summary>
    /// CommandProcessAsset
    /// </summary>
    public class CommandProcessAsset : ICommand
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
           
        }
    }
}
