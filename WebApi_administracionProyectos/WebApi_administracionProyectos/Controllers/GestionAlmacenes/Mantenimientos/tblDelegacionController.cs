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
    public class tblDelegacionController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblDelegacion
        public IQueryable<tbl_Delegacion> Gettbl_Delegacion()
        {
            return db.tbl_Delegacion;
        }

        // GET: api/tblDelegacion/5
        [ResponseType(typeof(tbl_Delegacion))]
        public object Gettbl_Delegacion(int opcion, string filtro)
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
                    res.data = obj_negocio.get_delegacionesCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    string codigoDelegacion = parametros[0].ToString().Trim();

                    if (db.tbl_Delegacion.Count(e => e.codigo_delegacion == codigoDelegacion) > 0)
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
                    int idDelegacion = Convert.ToInt32(parametros[0].ToString());

                    tbl_Delegacion objReemplazar;
                    objReemplazar = db.tbl_Delegacion.Where(u => u.id_Delegacion == idDelegacion).FirstOrDefault<tbl_Delegacion>();
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

        public object Puttbl_Delegacion(int id, tbl_Delegacion tbl_Delegacion)
        {
            Resultado res = new Resultado();

            tbl_Delegacion objReemplazar;
            objReemplazar = db.tbl_Delegacion.Where(u => u.id_Delegacion == id).FirstOrDefault<tbl_Delegacion>();

            objReemplazar.id_Empresa = tbl_Delegacion.id_Empresa;
            objReemplazar.codigo_delegacion = tbl_Delegacion.codigo_delegacion;
            objReemplazar.nombre_delegacion = tbl_Delegacion.nombre_delegacion;
            objReemplazar.estado = tbl_Delegacion.estado;
            objReemplazar.usuario_edicion = tbl_Delegacion.usuario_creacion;
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

        public object Posttbl_Delegacion(tbl_Delegacion tbl_Delegacion)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Delegacion.fecha_creacion = DateTime.Now;
                db.tbl_Delegacion.Add(tbl_Delegacion);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Delegacion.id_Delegacion;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }


        // DELETE: api/tblDelegacion/5
        [ResponseType(typeof(tbl_Delegacion))]
        public IHttpActionResult Deletetbl_Delegacion(int id)
        {
            tbl_Delegacion tbl_Delegacion = db.tbl_Delegacion.Find(id);
            if (tbl_Delegacion == null)
            {
                return NotFound();
            }

            db.tbl_Delegacion.Remove(tbl_Delegacion);
            db.SaveChanges();

            return Ok(tbl_Delegacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_DelegacionExists(int id)
        {
            return db.tbl_Delegacion.Count(e => e.id_Delegacion == id) > 0;
        }
    }
}