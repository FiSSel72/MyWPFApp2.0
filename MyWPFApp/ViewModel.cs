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
        private ObservableCollection<UserEventsListContext> _userEventContent;
        public string _login;
        public string _password;
        public string _newPassword;
        public string _isNewPassword;
        #endregion

        #region Commands
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(arg => SigninAsync()));
        public ICommand SwitchWindowCommand => _switchWindowCommand ?? (_switchWindowCommand = new RelayCommand(arg => SwitchWindow()));
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
                c.PostingChange(LoginStroke,PasswordStroke,NewPasswordStroke);
                PasswordStroke = NewPasswordStroke;
                UserChangedPass();
            }
        }
        private void SwitchWindow()
        {
            if (WindowType == TypeUserControl.Second)
            {
                UserEnter();
                WindowType = TypeUserControl.First;
                
            }
            else
            { 
                UserExit();
                ++WindowType;
                
            }
        }
        #endregion
    }
}
