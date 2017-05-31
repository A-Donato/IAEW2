﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TuriCorAPI.Controllers
{
    [EnableCors(origins: "http://localhost:2253", headers: "*", methods: "*")]
    public class PaisController : ApiController
    {
        public IHttpActionResult Get()
        {
            try
            {
                var cliente = new ServiceReferenceReservaVehiculos.WCFReservaVehiculosClient();

                var pais = cliente.ConsultarPaises();
                
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

    }
}