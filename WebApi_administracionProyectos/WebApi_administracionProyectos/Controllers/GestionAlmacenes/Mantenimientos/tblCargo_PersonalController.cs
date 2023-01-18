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
    public class tblCargo_PersonalController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblCargo_Personal
        public IQueryable<tbl_Cargo_Personal> Gettbl_Cargo_Personal()
        {
            return db.tbl_Cargo_Personal;
        }

        // GET: api/tblAlm_Materiales_Familia/5
        [ResponseType(typeof(tbl_Cargo_Personal))]
        public object Gettbl_Cargo_Personal(int opcion, string filtro)
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
                    res.data = obj_negocio.get_cargosCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idCargo = Convert.ToInt32(parametros[0].ToString());

                    tbl_Cargo_Personal objReemplazar;
                    objReemplazar = db.tbl_Cargo_Personal.Where(u => u.id_Cargo == idCargo).FirstOrDefault<tbl_Cargo_Personal>();
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
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    string nombreCargo = parametros[0].ToString().Trim();

                    if (db.tbl_Cargo_Personal.Count(e => e.nombre_cargo == nombreCargo) > 0)
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

        public object Puttbl_Cargo_Personal(int id, tbl_Cargo_Personal tbl_Cargo_Personal)
        {
            Resultado res = new Resultado();

            tbl_Cargo_Personal objReemplazar;
            objReemplazar = db.tbl_Cargo_Personal.Where(u => u.id_Cargo == id).FirstOrDefault<tbl_Cargo_Personal>();

            objReemplazar.nombre_cargo = tbl_Cargo_Personal.nombre_cargo;
            objReemplazar.estado = tbl_Cargo_Personal.estado;
            objReemplazar.usuario_edicion = tbl_Cargo_Personal.usuario_creacion;
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

        public object Posttbl_Cargo_Personal(tbl_Cargo_Personal tbl_Cargo_Personal)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Cargo_Personal.fecha_creacion = DateTime.Now;
                db.tbl_Cargo_Personal.Add(tbl_Cargo_Personal);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Cargo_Personal.id_Cargo;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblCargo_Personal/5
        [ResponseType(typeof(tbl_Cargo_Personal))]
        public IHttpActionResult Deletetbl_Cargo_Personal(int id)
        {
            tbl_Cargo_Personal tbl_Cargo_Personal = db.tbl_Cargo_Personal.Find(id);
            if (tbl_Cargo_Personal == null)
            {
                return NotFound();
            }

            db.tbl_Cargo_Personal.Remove(tbl_Cargo_Personal);
            db.SaveChanges();

            return Ok(tbl_Cargo_Personal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Cargo_PersonalExists(int id)
        {
            return db.tbl_Cargo_Personal.Count(e => e.id_Cargo == id) > 0;
        }
    }
}