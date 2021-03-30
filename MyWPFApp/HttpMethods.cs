using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWPFApp
{
    public class HttpMethods
    {
        public async void Posting(string log, string pass)
        {
             using (var client = new HttpClient())
            {   
                string user = JsonConvert.SerializeObject(
                   new
                   {
                       login = log,
                       password = pass
                   });
                var data = new StringContent(user, Encoding.UTF8, "application/json");
                var url = "http://bmhmh.ho.ua/index.php/api/login";

                HttpResponseMessage response = await client.PostAsync(url,data);
                response.EnsureSuccessStatusCode();

                var contents = await response.Content.ReadAsStringAsync();
                if (contents == "[\"Wrong login" + @"\" + "/password\"]")
                {
                    MessageBox.Show("Check it and try again", "Wrong login or password");
                }
                else MessageBox.Show(log, pass);
                

            }
            
        }
        
    }
}
