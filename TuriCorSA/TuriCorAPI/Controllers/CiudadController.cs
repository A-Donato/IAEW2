using AuthorizationServer.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TuriCorAPI.ServiceReference;

namespace TuriCorAPI.Controllers
{
    public class CiudadController : ApiController
    {

        //[Scope("read")]
        public IHttpActionResult Get(int id) 
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            try
            {
                
                var cliente = new WCFReservaVehiculosClient();

                var ciudades= cliente.ConsultarCiudades(credential,new ConsultarCiudadesRequest()
                {
                    IdPais = id,
                   
                });

                if (ciudades == null)
                {
                    return NotFound();
                }
                return Ok(ciudades);
            }
            catch (Exception ex)
            {
                
                return InternalServerError(ex);
            }
        }
        public IHttpActionResult Get(int idCiudad, int idPais)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";

            try
            {
                var cliente = new WCFReservaVehiculosClient();

                var ciudades = cliente.ConsultarCiudades(credential,new ConsultarCiudadesRequest()
                {
                    IdPais = idPais,

                });

               
                if (ciudades == null)
                {
                    return NotFound();
                }
                foreach (var c in ciudades.Ciudades)
                {
                    if (c.Id == idCiudad)
                    {
                        return Ok(c);
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
