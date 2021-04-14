using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWPFApp
{
    class TokenController
    {
        public static CurrentToken _token;
        public class Initialize
        {
            public CurrentToken currentToken { get; set; }
            public void Launch()
            {
                currentToken = TokenController.GetInstance();
            }
        }
        public static CurrentToken GetInstance()
        {
            if (_token == null)
            {
                _token = new CurrentToken();
            }
            return _token;
        }

    }
}
