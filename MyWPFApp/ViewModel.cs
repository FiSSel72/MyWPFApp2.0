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
        private ICommand _switchWindowCommand;
        private TypeUserControl _windowType;
        public string _login;
        public string _password;
        public string _data;
        #endregion

        #region Commands
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(arg => SigninAsync()));
        public ICommand SwitchWindowCommand => _switchWindowCommand ?? (_switchWindowCommand ?? (_switchWindowCommand = new RelayCommand(arg => SwitchWindow())));
        #endregion

        #region Properties
        public string LoginStroke
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("LoginStroke");
            }
        }
        public string PasswordStroke
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("PasswordStroke");
            }
        }
        public TypeUserControl WindowType
        {
            get { return _windowType; }
            set
            {
                _windowType = value;
                OnPropertyChanged(nameof(WindowType));
            }
        }
        #endregion

        #region Private Methods
        private void SigninAsync()
        {
            HttpMethods a = new HttpMethods();

            if (a.CheckIfCorrect(LoginStroke, PasswordStroke) == false)
            {
                MessageBox.Show("Check it and try again", "Wrong login or password");
            }
            else
            {
                _data = a.Posting(LoginStroke, PasswordStroke);
                SwitchWindow();
            }
        }
        private void SwitchWindow()
        {
            if (WindowType == TypeUserControl.Second)
                WindowType = TypeUserControl.First;
            else
                ++WindowType;
        }
        #endregion

        #region Public Methods
        #endregion
    }
}
