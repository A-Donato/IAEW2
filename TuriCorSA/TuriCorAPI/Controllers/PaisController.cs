using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using AuthorizationServer.App_Start;
using TuriCorAPI.ServiceReference;

namespace TuriCorAPI.Controllers
{
    [Authorize]
    //[Scope("read")]
    public class PaisController : ApiController
    {
        
        public IHttpActionResult Get()
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            try
            {

                var cliente = new WCFReservaVehiculosClient();

                var pais = cliente.ConsultarPaises(credential);
                
                if (pais == null)
                {
                    return NotFound();
                }
                return Ok(pais);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Get(int id)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            
            try
            {
                var cliente = new WCFReservaVehiculosClient();

                var pais = cliente.ConsultarPaises(credential);
               
              
                if (pais == null)
                {
                    return NotFound();
                }
                foreach (var p in pais.Paises)
                {
                    if (p.Id == id)
                    {
                        return Ok(p);
                    }
                }

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

    }
}
