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
    public class tblObra_TD_EmpresaController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblObra_TD_Empresa
        public IQueryable<tbl_Obra_TD_Empresa> Gettbl_Obra_TD_Empresa()
        {
            return db.tbl_Obra_TD_Empresa;
        }

        // GET: api/tblObra_TD_Empresa/5
        [ResponseType(typeof(tbl_Obra_TD_Empresa))]
        public IHttpActionResult Gettbl_Obra_TD_Empresa(int id)
        {
            tbl_Obra_TD_Empresa tbl_Obra_TD_Empresa = db.tbl_Obra_TD_Empresa.Find(id);
            if (tbl_Obra_TD_Empresa == null)
            {
                return NotFound();
            }

            return Ok(tbl_Obra_TD_Empresa);
        }

        // PUT: api/tblObra_TD_Empresa/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Obra_TD_Empresa(int id, tbl_Obra_TD_Empresa tbl_Obra_TD_Empresa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Obra_TD_Empresa.id_ObraTD_Empresa)
            {
                return BadRequest();
            }

            db.Entry(tbl_Obra_TD_Empresa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Obra_TD_EmpresaExists(id))
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

 
        public object Posttbl_Obra_TD_Empresa(tbl_Obra_TD_Empresa tbl_Obra_TD_Empresa)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Obra_TD_Empresa.fecha_creacion = DateTime.Now;
                db.tbl_Obra_TD_Empresa.Add(tbl_Obra_TD_Empresa);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Obra_TD_Empresa.id_ObraTD_Empresa;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }




        // DELETE: api/tblObra_TD_Empresa/5
        [ResponseType(typeof(tbl_Obra_TD_Empresa))]
        public IHttpActionResult Deletetbl_Obra_TD_Empresa(int id)
        {
            tbl_Obra_TD_Empresa tbl_Obra_TD_Empresa = db.tbl_Obra_TD_Empresa.Find(id);
            if (tbl_Obra_TD_Empresa == null)
            {
                return NotFound();
            }

            db.tbl_Obra_TD_Empresa.Remove(tbl_Obra_TD_Empresa);
            db.SaveChanges();

            return Ok(tbl_Obra_TD_Empresa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Obra_TD_EmpresaExists(int id)
        {
            return db.tbl_Obra_TD_Empresa.Count(e => e.id_ObraTD_Empresa == id) > 0;
        }
    }
}