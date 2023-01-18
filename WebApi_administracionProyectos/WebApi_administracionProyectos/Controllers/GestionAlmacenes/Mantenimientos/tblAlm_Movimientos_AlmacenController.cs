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
    public class tblAlm_Movimientos_AlmacenController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAlm_Movimientos_Almacen
        public IQueryable<tbl_Alm_Movimientos_Almacen> Gettbl_Alm_Movimientos_Almacen()
        {
            return db.tbl_Alm_Movimientos_Almacen;
        }

        // GET: api/tblAlm_Movimientos_Almacen/5
        [ResponseType(typeof(tbl_Alm_Movimientos_Almacen))]
        public object Gettbl_Alm_Movimientos_Almacen(int opcion, string filtro)
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
                    res.data = obj_negocio.get_movimientosAlmacenCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    string idMovAlmacen = parametros[0].ToString().Trim();

                    if (db.tbl_Alm_Movimientos_Almacen.Count(e => e.id_MovAlmacen == idMovAlmacen) > 0)
                    {
                        res.ok = true;
                        res.data = "OK";
                    }
                    else
                    {
                        res.ok = false;
                        res.data = "ERROR";
                    }
                    resul = res;
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    string idMovAlmacen = parametros[0].ToString().Trim();

                    tbl_Alm_Movimientos_Almacen objReemplazar;
                    objReemplazar = db.tbl_Alm_Movimientos_Almacen.Where(u => u.id_MovAlmacen == idMovAlmacen).FirstOrDefault<tbl_Alm_Movimientos_Almacen>();
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

        public object Puttbl_Alm_Movimientos_Almacen(string id, tbl_Alm_Movimientos_Almacen tbl_Alm_Movimientos_Almacen)
        {
            Resultado res = new Resultado();

            tbl_Alm_Movimientos_Almacen objReemplazar;
            objReemplazar = db.tbl_Alm_Movimientos_Almacen.Where(u => u.id_MovAlmacen == id).FirstOrDefault<tbl_Alm_Movimientos_Almacen>();
 
            objReemplazar.descripcion_MovAlmacen = tbl_Alm_Movimientos_Almacen.descripcion_MovAlmacen;
            objReemplazar.abreviatura_MovAlmacen = tbl_Alm_Movimientos_Almacen.abreviatura_MovAlmacen;
            objReemplazar.tipo_MovAlmacen = tbl_Alm_Movimientos_Almacen.tipo_MovAlmacen;
            objReemplazar.codigo_Sunat = tbl_Alm_Movimientos_Almacen.codigo_Sunat;

            objReemplazar.afectaStock_MovAlmacen = tbl_Alm_Movimientos_Almacen.afectaStock_MovAlmacen;
            objReemplazar.noAfectaStock_MovAlmacen = tbl_Alm_Movimientos_Almacen.noAfectaStock_MovAlmacen;
            objReemplazar.Anterior_Movimiento = tbl_Alm_Movimientos_Almacen.Anterior_Movimiento;

            objReemplazar.estado = tbl_Alm_Movimientos_Almacen.estado;
            objReemplazar.usuario_edicion = tbl_Alm_Movimientos_Almacen.usuario_creacion;
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

        public object Posttbl_Alm_Movimientos_Almacen(tbl_Alm_Movimientos_Almacen tbl_Alm_Movimientos_Almacen)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Alm_Movimientos_Almacen.fecha_creacion = DateTime.Now;
                db.tbl_Alm_Movimientos_Almacen.Add(tbl_Alm_Movimientos_Almacen);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Alm_Movimientos_Almacen.id_MovAlmacen;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }


        // DELETE: api/tblAlm_Movimientos_Almacen/5
        [ResponseType(typeof(tbl_Alm_Movimientos_Almacen))]
        public IHttpActionResult Deletetbl_Alm_Movimientos_Almacen(string id)
        {
            tbl_Alm_Movimientos_Almacen tbl_Alm_Movimientos_Almacen = db.tbl_Alm_Movimientos_Almacen.Find(id);
            if (tbl_Alm_Movimientos_Almacen == null)
            {
                return NotFound();
            }

            db.tbl_Alm_Movimientos_Almacen.Remove(tbl_Alm_Movimientos_Almacen);
            db.SaveChanges();

            return Ok(tbl_Alm_Movimientos_Almacen);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Alm_Movimientos_AlmacenExists(string id)
        {
            return db.tbl_Alm_Movimientos_Almacen.Count(e => e.id_MovAlmacen == id) > 0;
        }
    }
}