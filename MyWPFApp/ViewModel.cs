using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ICommand _isEqual;
        private TypeUserControl _windowType;
        private ObservableCollection<DataInfo> _datacontent;
        public string _login;
        public string _password;
        public string _newPassword;
        public string _isNewPassword;
        public string _data;
        #endregion

        #region Commands
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(arg => SigninAsync()));
        public ICommand SwitchWindowCommand => _switchWindowCommand ?? (_switchWindowCommand ?? (_switchWindowCommand = new RelayCommand(arg => SwitchWindow())));
        public ICommand IsEqualCommand => _isEqual ?? (_isEqual = new RelayCommand(arg => IsEqual()));
        #endregion

        #region Properties
        public ObservableCollection<DataInfo> DataContent
        {
            get { return _datacontent; }
            set { _datacontent = value; }
        }
        public ViewModel()
        {
            _datacontent = new ObservableCollection<DataInfo>();
            this.GenerateData();
        }
        public void GenerateData()
        {
            _datacontent.Add(new DataInfo("1002", "Khaleesi", "Mexico", "QUEEN", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1002", "Drogo", "Mexico", "DOTHRAKI", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1001", "Maria Anders", "Germany", "ALFKI", "Berlin"));
            _datacontent.Add(new DataInfo("1001", "Antonio Moreno", "Germany", "ANTON", "Berlin"));
            _datacontent.Add(new DataInfo("1001", "Martin Sommer", "Germany", "BOLID", "Berlin"));
            _datacontent.Add(new DataInfo("1001", "Sommer", "Germany", "BOL", "Berlin"));
            _datacontent.Add(new DataInfo("1001", "Jon Snow", "Germany", "BOLD", "Berlin"));
            _datacontent.Add(new DataInfo("1001", "Arya Stark", "Germany", "NOONE", "Berlin"));
            _datacontent.Add(new DataInfo("1002", "Ana Trujilo", "Mexico", "ANATR", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1002", "Styles", "Mexico", "HAHI", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1002", "Ana Trujilo", "Mexico", "ANATR", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1002", "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1002", "Christina Berglund", "Mexico", "ANATR", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Antonio Moreno", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Ronaldo", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Cersei", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Jack", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Antony", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Caesar", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Lanister", "Mexico", "ANTON", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1003", "Sansa", "Mexico", "BLAUS", "Mexico D.F."));
            _datacontent.Add(new DataInfo("1004", "Thomas Hardy", "UK", "AROUT", "London"));
            _datacontent.Add(new DataInfo("1005", "Christina Berglund", "Sweden", "BERGS", "Lula"));
            _datacontent.Add(new DataInfo("1005", "Hanna Moos", "Sweden", "BERGS", "Lula"));
            _datacontent.Add(new DataInfo("1006", "Hanna Moos", "Germany", "BLAUS", "Mannheim"));
            _datacontent.Add(new DataInfo("1007", "Frederique Citeaux", "France", "BLONP", "Strasbourg"));
            _datacontent.Add(new DataInfo("1007", "Martin Sommer", "France", "BOTTM", "Strasbourg"));
            _datacontent.Add(new DataInfo("1007", "Elizabeth Lincoln", "France", "BLONP", "Strasbourg"));
            _datacontent.Add(new DataInfo("1008", "Martin Sommer", "Spain", "BOLID", "Madrid"));
            _datacontent.Add(new DataInfo("1009", "Laurence Lebihan", "France", "BONAP", "Marseille"));
            _datacontent.Add(new DataInfo("1010", "Elizabeth Lincoln", "Canada", "BOTTM", "Tsawassen"));
        }
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
        public string NewPasswordStroke
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged("PasswordStroke");
            }
        }
        public string IsNewPasswordStroke
        {
            get { return _isNewPassword; }
            set
            {
                _isNewPassword = value;
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
        private void IsEqual()
        {
            if (PasswordStroke == NewPasswordStroke)
            {
                MessageBox.Show("New password must have diferences with old password");
            }
            else if (NewPasswordStroke != IsNewPasswordStroke)
            {
                MessageBox.Show("Password and repeated password have diferences");
            }
            else
            {
                HttpMethods c = new HttpMethods();
                c.PostingChange(LoginStroke, PasswordStroke, NewPasswordStroke);
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
