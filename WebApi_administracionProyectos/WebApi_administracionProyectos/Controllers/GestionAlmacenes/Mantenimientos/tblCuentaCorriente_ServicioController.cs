using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Entidades;
using Negocio.Resultados;

namespace WebApi_administracionProyectos.Controllers.GestionAlmacenes.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblCuentaCorriente_ServicioController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblCuentaCorriente_Servicio
        public IQueryable<tbl_CuentaCorriente_Servicio> Gettbl_CuentaCorriente_Servicio()
        {
            return db.tbl_CuentaCorriente_Servicio;
        }

        // GET: api/tblCuentaCorriente_Servicio/5
        [ResponseType(typeof(tbl_CuentaCorriente_Servicio))]
        public IHttpActionResult Gettbl_CuentaCorriente_Servicio(int id)
        {
            tbl_CuentaCorriente_Servicio tbl_CuentaCorriente_Servicio = db.tbl_CuentaCorriente_Servicio.Find(id);
            if (tbl_CuentaCorriente_Servicio == null)
            {
                return NotFound();
            }

            return Ok(tbl_CuentaCorriente_Servicio);
        }

        // PUT: api/tblCuentaCorriente_Servicio/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_CuentaCorriente_Servicio(int id, tbl_CuentaCorriente_Servicio tbl_CuentaCorriente_Servicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_CuentaCorriente_Servicio.id_CtaCte_Servicio)
            {
                return BadRequest();
            }

            db.Entry(tbl_CuentaCorriente_Servicio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_CuentaCorriente_ServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblCuentaCorriente_Servicio

        public object Posttbl_CuentaCorriente_Servicio(tbl_CuentaCorriente_Servicio tbl_CuentaCorriente_Servicio)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_CuentaCorriente_Servicio.fecha_creacion = DateTime.Now;
                db.tbl_CuentaCorriente_Servicio.Add(tbl_CuentaCorriente_Servicio);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_CuentaCorriente_Servicio.id_CtaCte_Servicio;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }




        // DELETE: api/tblCuentaCorriente_Servicio/5
        [ResponseType(typeof(tbl_CuentaCorriente_Servicio))]
        public IHttpActionResult Deletetbl_CuentaCorriente_Servicio(int id)
        {
            tbl_CuentaCorriente_Servicio tbl_CuentaCorriente_Servicio = db.tbl_CuentaCorriente_Servicio.Find(id);
            if (tbl_CuentaCorriente_Servicio == null)
            {
                return NotFound();
            }

            db.tbl_CuentaCorriente_Servicio.Remove(tbl_CuentaCorriente_Servicio);
            db.SaveChanges();

            return Ok(tbl_CuentaCorriente_Servicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CuentaCorriente_ServicioExists(int id)
        {
            return db.tbl_CuentaCorriente_Servicio.Count(e => e.id_CtaCte_Servicio == id) > 0;
        }
    }
}