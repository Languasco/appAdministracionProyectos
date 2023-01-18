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
using Negocio.GestionAlmacenes.Mantenimientos;
using Negocio.Resultados;

namespace WebApi_administracionProyectos.Controllers.GestionAlmacenes.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblAreasController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAreas
        public IQueryable<tbl_Areas> Gettbl_Areas()
        {
            return db.tbl_Areas;
        }

        // GET: api/tblAreas/5
        [ResponseType(typeof(tbl_Areas))]
        public object Gettbl_Areas(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idEstado = Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_areasCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idTipoAlmacen = Convert.ToInt32(parametros[0].ToString());

                    tbl_Areas objReemplazar;
                    objReemplazar = db.tbl_Areas.Where(u => u.id_Area == idTipoAlmacen).FirstOrDefault<tbl_Areas>();
                    objReemplazar.estado = 2;

                    db.Entry(objReemplazar).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                        res.ok = true;
                        res.data = "OK";
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        res.ok = false;
                        res.data = ex.InnerException.Message;
                    }
                    resul = res;

                }
                else
                {
                    res.ok = false;
                    res.data = "Opcion seleccionada invalida";
                    resul = res;
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;

                resul = res;
            }
            return resul;
        }

        public object Puttbl_Areas(int id, tbl_Areas tbl_Areas)
        {
            Resultado res = new Resultado();

            tbl_Areas objReemplazar;
            objReemplazar = db.tbl_Areas.Where(u => u.id_Area == id).FirstOrDefault<tbl_Areas>();

            objReemplazar.nombre_area = tbl_Areas.nombre_area;
            objReemplazar.estado = tbl_Areas.estado;
            objReemplazar.usuario_edicion = tbl_Areas.usuario_creacion;
            objReemplazar.fecha_edicion = DateTime.Now;

            db.Entry(objReemplazar).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                res.ok = true;
                res.data = "OK";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;
            }

            return res;
        }

        public object Posttbl_Areas(tbl_Areas tbl_Areas)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Areas.fecha_creacion = DateTime.Now;
                db.tbl_Areas.Add(tbl_Areas);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Areas.id_Area;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblAreas/5
        [ResponseType(typeof(tbl_Areas))]
        public IHttpActionResult Deletetbl_Areas(int id)
        {
            tbl_Areas tbl_Areas = db.tbl_Areas.Find(id);
            if (tbl_Areas == null)
            {
                return NotFound();
            }

            db.tbl_Areas.Remove(tbl_Areas);
            db.SaveChanges();

            return Ok(tbl_Areas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_AreasExists(int id)
        {
            return db.tbl_Areas.Count(e => e.id_Area == id) > 0;
        }
    }
}