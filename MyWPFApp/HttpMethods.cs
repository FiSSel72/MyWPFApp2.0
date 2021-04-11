using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyWPFApp
{
   
    public class HttpMethods
    {
        CurrentToken _t;
        public string Posting(string log, string pass)
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
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 200)
            {        
                var content = response.Content;
                if (content == "{\"response\":\"Wrong login" + @"\" + "/password!\"}")
                {
                    return "Wrong pass";
                }
                else
                {
                    _t = JsonConvert.DeserializeObject<CurrentToken>(content);
                    return content;
                }
            }
            else
            { 
                return "No Connection";
            }
        }
        public string GetData()
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/getRecords", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       token = _t.token
                   }));
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return content;
        }
        public void PostingChange(string log, string pass, string pass2)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/NewPassword", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       login = log,
                       password = pass,
                       password2= pass2
                   }));

            IRestResponse response = client.Execute(request);
            MessageBox.Show("Password changed");

        }

    }
}
