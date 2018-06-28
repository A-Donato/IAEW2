using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TuriCorAPI.Models;
using AuthorizationServer.App_Start;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using TuriCorAPI.ServiceReference;

namespace TuriCorAPI.Controllers
{
    //[EnableCors(origins: "http://localhost:8660", headers: "*", methods: "*")]
    //[Scope("read")]
    public class ReservaController : ApiController
    {
        private TuricorEntities _db = new TuricorEntities();

        public IHttpActionResult Get()
        {
            try
            {
                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                return Ok(_db.Reserva);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                Reserva res = _db.Reserva.FirstOrDefault(p => p.Id == id);
                if (res == null)
                {
                    return NotFound();
                }
                return Ok(res);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Get(bool incluirCancel)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            try
            {
                var cliente = new WCFReservaVehiculosClient();

                var reservas = cliente.ConsultarReserva(credential,new ConsultarReservasRequest()
                );

                if (reservas == null)
                {
                    return NotFound();
                }
                return Ok(reservas);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        [HttpPost]
        public IHttpActionResult Post([FromBody]SuperReserva res)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            try
            {
                var cliente = new WCFReservaVehiculosClient();

                var reserva = cliente.ReservarVehiculo(credential, new ReservarVehiculoRequest()
                {
                    ApellidoNombreCliente = res.ApellidoNombreCliente,
                    FechaHoraDevolucion = res.FechaHoraDevolucion,
                    FechaHoraRetiro = res.FechaHoraRetiro,
                    IdVehiculoCiudad = res.IdVehiculoCiudad,
                    LugarDevolucion = (LugarRetiroDevolucion)Enum.Parse(typeof(LugarRetiroDevolucion), res.LugarDevolucion),
                    LugarRetiro = (LugarRetiroDevolucion)Enum.Parse(typeof(LugarRetiroDevolucion), res.LugarRetiro),
                    NroDocumentoCliente = res.NroDocumentoCliente,

                });


                Reserva reser = new Reserva()
                {
                    CodigoReserva = reserva.Reserva.CodigoReserva,
                    //CodigoReserva = "EUVMH",
                    FechaReserva = res.FechaReserva,
                    IdCliente = res.IdCliente,
                    IdVendedor = res.IdVendedor,
                    Costo = res.Costo,
                    PrecioVenta = res.PrecioVenta,
                    IdVehiculoCiudad = res.IdVehiculoCiudad,
                    IdCiudad = res.IdCiudad,
                    IdPais = res.IdPais


                };

                if (_db.Reserva == null || !_db.Reserva.Any())
                {
                    return NotFound();
                }
                if (res == null)
                {
                    return BadRequest();
                }

                _db.Reserva.Add(reser);

                _db.SaveChanges();


                return Created("api/Reserva/" + reser.Id, reser);

            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody]Reserva res)
        {
            try
            {
                if (res == null)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (id != res.Id)
                {
                    return BadRequest();
                }
                if (_db.Reserva.Count(e => e.Id == id) == 0)
                {
                    return NotFound();
                }
                _db.Entry(res).State = System.Data.Entity.EntityState.Modified;

                _db.SaveChanges();

                return Ok(res);


            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int id)
        {
            var credential = new Credentials();
            credential.UserName = "grupo_nro7";
            credential.Password = "WSbRKVdf";
            try
            {
                var res = _db.Reserva.Find(id);
                if (res == null)
                {
                    return NotFound();
                }
                var cliente = new WCFReservaVehiculosClient();

                _db.Reserva.Remove(res);

                _db.SaveChanges();

                var reserva = cliente.CancelarReserva(credential, new CancelarReservaRequest()
                {
                    CodigoReserva = res.CodigoReserva
                });

                return Ok(res);

            }
            catch (EntityException ex)
            {

                return InternalServerError(ex);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
