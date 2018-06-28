using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguridadOAuth
{
    public class Cliente
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string RedirectUrl { get; set; }
    }
    public static class Credencial
    {
        public readonly static Cliente cliente = new Cliente
        {
            Id = "grupo_nro7_client",
            Secret = "test_secret",
            RedirectUrl = Rutas.AuthorizeCodeCallBackPath
        };

        
    }
}
