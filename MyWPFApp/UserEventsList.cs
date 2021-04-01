using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    public class UserEventsList
    {
        public int Id { get; set; }
        public string User_login { get; set; }
        public string Event_id { get; set; }
        public string Datetime { get; set; } = (DateTime.Now).ToString();
    }
}
