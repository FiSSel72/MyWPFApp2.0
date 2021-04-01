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
        public bool CheckIfCorrect(string log, string pass)
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
                if (content == "[\"Wrong login" + @"\" + "/password\"]")
                {
                    return false;
                }
                else return true;
            }
            else 
            {
                MessageBox.Show("No Connection");
                return false;
            }
        }
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
                return content;
            }
            else
            {
                MessageBox.Show("No Connection");
                return "No Connection";
            }
            
        }
        public void PostingChange(string log, string pass, string pass2)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/login", Method.POST);

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
