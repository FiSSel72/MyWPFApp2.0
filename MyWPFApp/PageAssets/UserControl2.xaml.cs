using Newtonsoft.Json;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWPFApp.PageAssets
{
    /// <summary>
    /// Логика взаимодействия для UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        UserEventsListContext db;
        public UserControl2()
        {
            InitializeComponent();
            
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.WindowState = WindowState.Minimized;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            
            using (UserEventsListContext db = new UserEventsListContext())
            {
                string s=UserLoginBlock.Text;
                UserEventsList UserEvent = new UserEventsList { User_login = s, Event_id = "1" };
                db.UserEvents.Add(UserEvent);
                db.SaveChanges();
            }
            System.Windows.Application.Current.Shutdown(-1);
        }

        private void PasswordToTag(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            passwordBox.Tag = passwordBox.Password;
        }

        private void ButtonReports_Click(object sender, RoutedEventArgs e)
        {
            HttpMethods a = new HttpMethods();
            string p = UserLoginBlock.Text;
            string l = LoginText.Text;
            InfoGrid.ItemsSource= JsonConvert.DeserializeObject<ObservableCollection<DataInfo>>(a.Posting(p, l));
        }

        private void ButtonLocalDb_Click(object sender, RoutedEventArgs e)
        {
            db = new UserEventsListContext();
            db.UserEvents.Load();
            InfoGrid.ItemsSource = db.UserEvents.Local.ToBindingList();
        }
    }
}
