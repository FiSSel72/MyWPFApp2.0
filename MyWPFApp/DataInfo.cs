using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    public class DataInfo
    {
        string _date;
        string _time;
        string _phone;
        string _event;
        string _name;
        public string Date 
        { 
            get 
            {
                return _date;
            }
            set 
            {
                _date= DataCryptography.DecryptString(value, DataCryptography.key, DataCryptography.iv);
            } 
        }
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = DataCryptography.DecryptString(value, DataCryptography.key, DataCryptography.iv);
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = DataCryptography.DecryptString(value, DataCryptography.key, DataCryptography.iv);
            }
        }
        public string Event
        {
            get
            {
                return _event;
            }
            set
            {
                _event = DataCryptography.DecryptString(value, DataCryptography.key, DataCryptography.iv);
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = DataCryptography.DecryptString(value, DataCryptography.key, DataCryptography.iv);
            }
        }


    }
}
