using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using AuthorizationServer.App_Start;
using System.Collections.Generic;
using TuriCorAPI.ServiceReference;

namespace TuriCorAPI.Controllers
{


    public class VehiculoController : ApiController
    {
        //[Scope("read")]
        public IHttpActionResult Get(int Id, DateTime fechaHoraRetiro, DateTime fechaHoraDevolucion)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";

            List<VehiculoModel> listaVehiculos = new List<VehiculoModel>();
            try
            {
                var cliente = new WCFReservaVehiculosClient();

                var vehiculos = cliente.ConsultarVehiculosDisponibles(credential, new ConsultarVehiculosRequest()
                {
                    IdCiudad = Id,
                    FechaHoraRetiro = fechaHoraRetiro,
                    FechaHoraDevolucion = fechaHoraDevolucion
                });

                if (vehiculos == null)
                {
                    return NotFound();
                }
                foreach (VehiculoModel ve in vehiculos.VehiculosEncontrados)
                {
                    ve.PrecioPorDia = ve.PrecioPorDia * (decimal)1.20;

                    listaVehiculos.Add(ve);
                }

                return Ok(vehiculos);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }




    }
}
