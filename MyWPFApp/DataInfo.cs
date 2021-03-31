using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    public class DataInfo
    {
        string date;
        string time;
        string phone;
        string userEvent;
        string name;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string UserEvent
        {
            get { return userEvent; }
            set { userEvent = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DataInfo(string date, string time, string phone, string userEvent, string name)
        {
            this.Date = date;
            this.Time = time;
            this.Phone = phone;
            this.UserEvent = userEvent;
            this.Name = name;
        }
    }
}
