using Newtonsoft.Json;
using RestSharp;
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
        public void Posting(string log, string pass)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/login", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       login = log,
                       password = pass
                   }));

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (content == "[\"Wrong login" + @"\" + "/password\"]")
                {
                    MessageBox.Show("Check it and try again", "Wrong login or password");
                }
                else MessageBox.Show(content);
                
            
        }
        
    }
}
