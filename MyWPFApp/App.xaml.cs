using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyWPFApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDIwMTg4QDMxMzkyZTMxMmUzME1JcGhrWlVneTRzM0pRbUtudXRlRHUzR1krM0JTcDg1RXZRaEllK09JV1k9");
        }
    }
}
