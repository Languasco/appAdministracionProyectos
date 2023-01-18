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
    public class tblAlm_Tipos_AlmacenController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAlm_Tipos_Almacen
        public IQueryable<tbl_Alm_Tipos_Almacen> Gettbl_Alm_Tipos_Almacen()
        {
            return db.tbl_Alm_Tipos_Almacen;
        }

        // GET: api/tblAlm_Tipos_Almacen/5
        [ResponseType(typeof(tbl_Alm_Tipos_Almacen))]
        // GET: api/tblLocales/5
        public object Gettbl_Alm_Tipos_Almacen(int opcion, string filtro)
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
                    res.data = obj_negocio.get_tiposAlmacenCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idTipoAlmacen = Convert.ToInt32(parametros[0].ToString());

                    tbl_Alm_Tipos_Almacen objReemplazar;
                    objReemplazar = db.tbl_Alm_Tipos_Almacen.Where(u => u.id_TipoAlmacen == idTipoAlmacen).FirstOrDefault<tbl_Alm_Tipos_Almacen>();
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

        public object Puttbl_Alm_Tipos_Almacen(int id, tbl_Alm_Tipos_Almacen tbl_Alm_Tipos_Almacen)
        {
            Resultado res = new Resultado();

            tbl_Alm_Tipos_Almacen objReemplazar;
            objReemplazar = db.tbl_Alm_Tipos_Almacen.Where(u => u.id_TipoAlmacen == id).FirstOrDefault<tbl_Alm_Tipos_Almacen>();

            objReemplazar.nombre_TipoAlmacen = tbl_Alm_Tipos_Almacen.nombre_TipoAlmacen;
            objReemplazar.estado = tbl_Alm_Tipos_Almacen.estado;
            objReemplazar.usuario_edicion = tbl_Alm_Tipos_Almacen.usuario_creacion;
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

        public object Posttbl_Alm_Tipos_Almacen(tbl_Alm_Tipos_Almacen tbl_Alm_Tipos_Almacen)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Alm_Tipos_Almacen.fecha_creacion = DateTime.Now;
                db.tbl_Alm_Tipos_Almacen.Add(tbl_Alm_Tipos_Almacen);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Alm_Tipos_Almacen.id_TipoAlmacen;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblAlm_Tipos_Almacen/5
        [ResponseType(typeof(tbl_Alm_Tipos_Almacen))]
        public IHttpActionResult Deletetbl_Alm_Tipos_Almacen(int id)
        {
            tbl_Alm_Tipos_Almacen tbl_Alm_Tipos_Almacen = db.tbl_Alm_Tipos_Almacen.Find(id);
            if (tbl_Alm_Tipos_Almacen == null)
            {
                return NotFound();
            }

            db.tbl_Alm_Tipos_Almacen.Remove(tbl_Alm_Tipos_Almacen);
            db.SaveChanges();

            return Ok(tbl_Alm_Tipos_Almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Alm_Tipos_AlmacenExists(int id)
        {
            return db.tbl_Alm_Tipos_Almacen.Count(e => e.id_TipoAlmacen == id) > 0;
        }
    }
}