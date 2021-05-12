using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MyWPFApp
{
    class ViewModel : ViewModelBase, IDataErrorInfo
    {

        #region Fields
        private ICommand _loginCommand;
        private ICommand _registerCommand;
        private ICommand _switchWindowCommand;
        private ICommand _switchRegisterCommand;
        private ICommand _isEqual;
        private TypeUserControl _windowType;
        private ObservableCollection<DataInfo> _datacontent;
        private ObservableCollection<UserEventsListContext> _userEventContent;
        public string _login;
        public string _password;
        public string _email;
        public string _name;
        public string _surname;
       // public bool _isNoErrors;
        public bool _isNewPassEqual;
        public string _newPassword;
        public string _isNewPassword;
        public Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        Regex _hasNumber = new Regex(@"[0-9]+");
        Regex _hasUpperChar = new Regex(@"[A-Z]+");
        Regex _hasMinimum = new Regex(@".{8,}");
        #endregion

        #region Commands
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(arg => SigninAsync()));
        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new RelayCommand(arg => SignupAsync()));
        public ICommand SwitchWindowCommand => _switchWindowCommand ?? (_switchWindowCommand = new RelayCommand(arg => SwitchWindow()));
        public ICommand SwitchRegisterCommand => _switchRegisterCommand ?? (_switchRegisterCommand = new RelayCommand(arg => SwitchRegister()));
        public ICommand IsEqualCommand => _isEqual ?? (_isEqual = new RelayCommand(arg => IsEqual()));
        #endregion

        #region Properties
        public ObservableCollection<DataInfo> DataContent
        {
            get { return _datacontent; }
            set
            {
                _datacontent = value;
                OnPropertyChanged(nameof(DataContent));
            }
        }
        public ObservableCollection<UserEventsListContext> UserEventContent
        {
            get { return _userEventContent; }
            set
            {
                _userEventContent = value;
                OnPropertyChanged(nameof(UserEventContent));
            }
        }

        public string LoginStroke
        {
            get { return _login; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorCollection["LoginStroke"] = "LoginStroke cannot be empty";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Login cannot be empty.(Exception)");
                }
                else if (value.Length < 5)
                {
                    ErrorCollection["LoginStroke"] = "LoginStroke must be a minimum of 5 characters.";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Login may contain at least 5 characters.(Exception)");
                }
                else
                {
                    _login = value;
                    OnPropertyChanged("LoginStroke");
                }
            }
        }
        public string PasswordStroke
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorCollection["PasswordStroke"] = "PasswordStroke cannot be empty";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Password cannot be empty.(Exception)");
                }
                else if (_hasMinimum.IsMatch(value) == false)
                {
                    ErrorCollection["PasswordStroke"] = "PasswordStroke must contain at least 8 characters";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Password must contain at least 8 characters.(Exception)");
                }
                else if (_hasNumber.IsMatch(value) == false)
                {
                    ErrorCollection["PasswordStroke"] = "PasswordStroke must contain at least one number";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Password must contain at least one number.(Exception)");
                }
                else if (_hasUpperChar.IsMatch(value) == false)
                {
                    ErrorCollection["PasswordStroke"] = "PasswordStroke must contain at least one upper character";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Passwor must contain at least one upper character.(Exception)");
                }
                else
                {
                    _password = value;
                    OnPropertyChanged("PasswordStroke");
                }
            }
        }
        public string NewPasswordStroke
        {
            get { return _newPassword; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPasswordStroke));
                    OnPropertyChanged(nameof(IsNewPassEqual));
                    throw new ArgumentException("New Password cannot be empty.(Exception)");
                }
                else if (_hasMinimum.IsMatch(value) == false)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPasswordStroke));
                    OnPropertyChanged(nameof(IsNewPassEqual));
                    throw new ArgumentException("New Password must contain at least 8 characters.(Exception)");
                }
                else if (_hasNumber.IsMatch(value) == false)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPasswordStroke));
                    OnPropertyChanged(nameof(IsNewPassEqual));
                    throw new ArgumentException("New Password must contain at least one number.(Exception)");
                }
                else if (_hasUpperChar.IsMatch(value) == false)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPasswordStroke));
                    OnPropertyChanged(nameof(IsNewPassEqual));
                    throw new ArgumentException("New Password must contain at least one upper character.(Exception)");
                }
                    
                _newPassword = value;
                OnPropertyChanged(nameof(NewPasswordStroke));
                OnPropertyChanged(nameof(IsNewPassEqual));
                

            }
        }
        public string IsNewPasswordStroke
        {
            get { return _isNewPassword; }
            set
            {
                _isNewPassword = value;
                OnPropertyChanged("IsNewPasswordStroke");
                OnPropertyChanged(nameof(IsNewPassEqual));
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
        public string EmailStroke
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorCollection["EmailStroke"] = "EmailStroke cannot be empty";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Email cannot be empty.(Exception)");
                }
                else if (regex.IsMatch(value) == false)
                {
                    ErrorCollection["EmailStroke"] = "EmailStroke must be an email";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Not an email");
                }
                else
                {
                    _email = value;
                    OnPropertyChanged("EmailStroke");
                }
            }
        }
        public string NameStroke
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorCollection["NameStroke"] = "NameStroke cannot be empty";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Name cannot be empty.(Exception)");
                }
                else if (value.Length < 3)
                {
                    ErrorCollection["NameStroke"] = "NameStroke must be a minimum of 3 characters.";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Name may contain at least 3 characters.(Exception)");
                }
                else
                {
                    _name = value;
                    OnPropertyChanged("NameStroke");
                }
            }
        }
        public string SurnameStroke
        {
            get { return _surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ErrorCollection["SurnameStroke"] = "SurnameStroke cannot be empty";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Surname cannot be empty.(Exception)");
                }
                else if (value.Length < 5)
                {
                    ErrorCollection["SurnameStroke"] = "SurnameStroke must be a minimum of 5 characters.";
                    OnPropertyChanged(nameof(ErrorCollection));
                    OnPropertyChanged(nameof(IsNoErrors));
                    throw new ArgumentException("Surname may contain at least 5 characters.(Exception)");
                }
                else
                {
                    _surname = value;
                    OnPropertyChanged("SurnameStroke");
                }
            }
        }
        public bool IsNoErrors => ErrorCollection.All(error => string.IsNullOrWhiteSpace(error.Value));
        public bool IsNewPassEqual
        {
            get
            {
                if (string.IsNullOrWhiteSpace(NewPasswordStroke) == true)
                {
                    _isNewPassEqual = false;
                }
                else if (_hasMinimum.IsMatch(NewPasswordStroke) & _hasNumber.IsMatch(NewPasswordStroke) & _hasUpperChar.IsMatch(NewPasswordStroke) & String.Equals(NewPasswordStroke, IsNewPasswordStroke) & String.Equals(PasswordStroke, NewPasswordStroke) == false)
                {
                    _isNewPassEqual = true;
                }
                else
                {
                    _isNewPassEqual = false;
                }
                
                return _isNewPassEqual;
            }
            set{}
        }

        public string Error { get { return null; } }
        public Dictionary<string, string> ErrorCollection { get; set; } = new Dictionary<string, string>();
        public string this[string name]
        {
            get
            {
                 string result = null;
                 switch (name)
                 {
                     case "NameStroke":
                         if (string.IsNullOrWhiteSpace(NameStroke) == true)
                         {
                             result = "NameStroke cannot be empty";
                         }
                         else if (NameStroke.Length < 3)
                         {
                             result = "NameStroke must be a minimum of 3 characters.";
                         }
                         break;
                     case "SurnameStroke":
                         if (string.IsNullOrWhiteSpace(SurnameStroke) == true)
                         { 
                             result = "SurnameStroke cannot be empty";
                         }
                         else if (SurnameStroke.Length < 4)
                         { 
                             result = "SurnameStroke must be a minimum of 4 characters.";
                         }
                         break;
                     case "EmailStroke":
                         if (string.IsNullOrWhiteSpace(EmailStroke))
                         {
                             result = "EmailStroke cannot be empty";
                         }
                         else if (regex.IsMatch(EmailStroke) == false)
                         {
                             result = "EmailStroke must be an email";
                         }
                         break;
                     case "PasswordStroke":
                         if (string.IsNullOrWhiteSpace(PasswordStroke))
                         {
                             result = "PasswordStroke cannot be empty";
                         }
                         else if (_hasMinimum.IsMatch(PasswordStroke) == false)
                         {
                             result = "PasswordStroke must contain at least 8 characters";
                         }
                         else if (_hasNumber.IsMatch(PasswordStroke) == false)
                         {
                             result = "PasswordStroke must contain at least one number";
                         }
                         else if (_hasUpperChar.IsMatch(PasswordStroke)==false)
                         {
                             result = "PasswordStroke must contain at least one upper character";
                         }
                         break;
                     case "LoginStroke":
                         if (string.IsNullOrWhiteSpace(LoginStroke))
                         {
                             result = "LoginStroke cannot be empty";
                         }
                         else if (LoginStroke.Length<5)
                         {
                             result = "LoginStroke must be a minimum of 5 characters.";
                         }
                         break;
                  

                 }

                ErrorCollection[name] = result;
                OnPropertyChanged(nameof(ErrorCollection));
                OnPropertyChanged(nameof(IsNoErrors));
                return result;
            }
            set {}
         }
        
                #endregion

                #region Private Methods
                private void UserEnter()
        {
            using(UserEventsListContext db =new UserEventsListContext())
            {
                UserEventsList UserEvent = new UserEventsList { User_login = LoginStroke, Event_id="0" };
                db.UserEvents.Add(UserEvent);
                db.SaveChanges();
            }
        }
        public void UserExit()
        {
            using (UserEventsListContext db = new UserEventsListContext())
            {
                UserEventsList UserEvent = new UserEventsList { User_login = LoginStroke, Event_id = "1" };
                db.UserEvents.Add(UserEvent);
                db.SaveChanges();
            }
        }
        private void UserChangedPass()
        {
            using (UserEventsListContext db = new UserEventsListContext())
            {
                UserEventsList UserEvent = new UserEventsList { User_login = LoginStroke, Event_id = "2" };
                db.UserEvents.Add(UserEvent);
                db.SaveChanges();
            }
        }
        private void SigninAsync()
        {
            HttpMethods a = new HttpMethods();
            var content = a.Posting(LoginStroke, PasswordStroke);

            if (content == "No Connection")
            {
                MessageBox.Show("No Connection");
            }
            else if (content == "Wrong pass")
            {
                MessageBox.Show("Check it and try again", "Wrong login or password");
            }
            else
            {
                DataContent = JsonConvert.DeserializeObject<ObservableCollection<DataInfo>>(a.GetData(content));
                SwitchWindow();
            }
        }
        private void SignupAsync()
        {
            HttpMethods a = new HttpMethods();
            var content = a.Register(NameStroke,SurnameStroke,EmailStroke,LoginStroke,PasswordStroke);
            if(content== "Done")
            {
                MessageBox.Show("Done");
                SwitchRegister();
            }
            else
            {
                MessageBox.Show(content);
            }
        }
        private bool IsEqual()
        {
            if (PasswordStroke == NewPasswordStroke)
            {
                MessageBox.Show("New password must have diferences with old password");
                return false;
            }
            else if (NewPasswordStroke != IsNewPasswordStroke)
            {
                MessageBox.Show("Password and repeated password have diferences");
                return false;
            }
            else
            {
                HttpMethods c = new HttpMethods();
                c.PostingChange(LoginStroke,PasswordStroke,NewPasswordStroke);
                PasswordStroke = NewPasswordStroke;
                UserChangedPass();
                return true;
            }
        }
        private void SwitchWindow()
        {
            if (WindowType == TypeUserControl.Second)
            {
                UserExit();
                WindowType = TypeUserControl.First;
                
            }
            else
            { 
                UserEnter();
                WindowType=TypeUserControl.Second;
                
            }
        }
        private void SwitchRegister()
        {
            if (WindowType == TypeUserControl.Third)
            {
                WindowType = TypeUserControl.First;

            }
            else
            {
                WindowType = TypeUserControl.Third;

            }
        }
        #endregion
    }
}
