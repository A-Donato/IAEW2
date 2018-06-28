using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguridadOAuth
{
    public static class Rutas
    {
        public const string AuthorizationServerBaseAddress = "http://130.211.183.120:8080";

        public const string ResourceServerBaseAddress = "http://localhost:2253/";

        public const string AuthorizeCodeCallBackPath = "http://localhost:8660/Account/CallBack";

        public const string AuthorizePath = "http://ec2-54-87-197-49.compute-1.amazonaws.com/web/authorize";
        public const string TokenPath = "http://ec2-54-87-197-49.compute-1.amazonaws.com/v1/oauth/tokens";
        public const string MePath = "/api/Me";
    }
}
