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
        
        public string Posting(string log, string pass)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/login", Method.POST);
            
            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       login = DataCryptography.EncryptString(log, DataCryptography.key, DataCryptography.iv),
                       password = DataCryptography.EncryptString(pass, DataCryptography.key, DataCryptography.iv)
                   }));

            IRestResponse response = client.Execute(request);
            HttpStatusCode statusCode = response.StatusCode;
            int numericStatusCode = (int)statusCode;
            if (numericStatusCode == 200)
            {        
                var content = response.Content;
                if (content == "{\"response\":\"Wrong login" + "/password!\"}")
                {
                    return "Wrong pass";
                }
                else
                {
                    TokenController._token = JsonConvert.DeserializeObject<CurrentToken>(content);
                    TokenController._token.token = DataCryptography.DecryptString(TokenController._token.token, DataCryptography.key, DataCryptography.iv);

                    return TokenController.GetInstance().token;
                }
            }
            else
            { 
                return "No Connection";
            }
        }
        public string GetData(string _t)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/getRecords", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       token = DataCryptography.EncryptString(_t, DataCryptography.key, DataCryptography.iv)
                   }));
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return content;
        }
        public string Register(string firstname, string secondname, string mail, string username,string pass )
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/NewUser", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       login = DataCryptography.EncryptString(username, DataCryptography.key, DataCryptography.iv),
                       password = DataCryptography.EncryptString(pass, DataCryptography.key, DataCryptography.iv),
                       email = DataCryptography.EncryptString(mail, DataCryptography.key, DataCryptography.iv),
                       name = DataCryptography.EncryptString(firstname, DataCryptography.key, DataCryptography.iv),
                       surname = DataCryptography.EncryptString(secondname, DataCryptography.key, DataCryptography.iv)

                   }));
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (content== "{\"response\":\"This login already exists!\"}")
            {
                return "This login already exists!";
            }
            return "Done";
        }
        public void PostingChange(string log, string pass, string pass2)
        {
            var client = new RestClient(" http://bmhmh.ho.ua");
            var request = new RestRequest("/index.php/api/NewPassword", Method.POST);

            request.AddJsonBody(JsonConvert.SerializeObject(
                   new
                   {
                       login = DataCryptography.EncryptString(log, DataCryptography.key, DataCryptography.iv),
                       password = DataCryptography.EncryptString(pass, DataCryptography.key, DataCryptography.iv),
                       password2 = DataCryptography.EncryptString(pass2, DataCryptography.key, DataCryptography.iv)
                   }));

            IRestResponse response = client.Execute(request);
            var result= response.Content;
            MessageBox.Show(result);

        }

    }
}
