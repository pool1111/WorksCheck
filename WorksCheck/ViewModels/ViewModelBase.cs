using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorksCheck.ViewModels
{
    #region ** Class : ViewModelBase
    public abstract class ViewModelBase : INotifyPropertyChanged,IDataErrorInfo
    {
        #region == implement of INotifyPropertyChanged ==

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region == implement of IDataErrorInfo==
        // IDataErrorInfo用のエラーメッセージを保持する辞書
        private Dictionary<string, string> _ErrorMessages = new Dictionary<string, string>();

        // IDataErrorInfo.Error の実装
        string IDataErrorInfo.Error
        {
            get { return (_ErrorMessages.Count > 0) ? "Has Error" : null; }
        }

        // IDataErrorInfo.Item の実装
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (_ErrorMessages.ContainsKey(columnName))
                    return _ErrorMessages[columnName];
                else
                    return null;
            }
        }

        // エラーメッセージのセット
        protected void SetError(string propertyName, string errorMessage)
        {
            _ErrorMessages[propertyName] = errorMessage;
        }

        // エラーメッセージのクリア
        protected void ClearErrror(string propertyName)
        {
            if (_ErrorMessages.ContainsKey(propertyName))
                _ErrorMessages.Remove(propertyName);
        }

        #endregion

        #region == implant of ICommand Helper == 
        #region ** Class : _DelegateCommand

        private class _DelegateCommand : ICommand
        {
            private Action<object> _Command;
            private Func<object,bool> _CanExecute;

            public _DelegateCommand(Action<object> command,Func<object,bool> canExecute = null)
            {
                if (command == null)
                    throw new ArgumentNullException();

                _Command = command;
                _CanExecute = canExecute;
            }

            void ICommand.Execute(object parameter)
            {
                _Command(parameter);
            }

            bool ICommand.CanExecute(object parameter)
            {
                if(_CanExecute != null)
                    return _CanExecute(parameter);
                else
                    return true;
            }

            event EventHandler ICommand.CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
        #endregion

        protected ICommand CreateCommand(Action<object> command,Func<object,bool> canExecute)
        {
            return new _DelegateCommand(command, canExecute);
        }
        #endregion
    }
#endregion
}
