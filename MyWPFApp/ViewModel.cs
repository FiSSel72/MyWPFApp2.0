using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyWPFApp
{
    class ViewModel : ViewModelBase
    {
        #region Fields
        private ICommand _loginCommand;
        private string _login;
        private string _password;
        #endregion

        #region Commands
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(arg => SigninAsync()));
        #endregion

        #region Properties
        public string LoginStroke
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged(nameof(LoginStroke));
            }
        }
        public string PasswordStroke
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PasswordStroke));
            }
        }
        #endregion

        #region Private Methods
        private void SigninAsync()
        {
            HttpMethods.Posting(LoginStroke,PasswordStroke);
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
