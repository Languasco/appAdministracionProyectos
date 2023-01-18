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
    public class tblAlm_AlmacenesController : ApiController
    {

        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAlm_Almacenes
        public IQueryable<tbl_Alm_Almacenes> Gettbl_Alm_Almacenes()
        {
            return db.tbl_Alm_Almacenes;
        }

        // GET: api/tblAlm_Almacenes/5
        [ResponseType(typeof(tbl_Alm_Almacenes))]
        public object Gettbl_Alm_Almacenes(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');

                    int delegacion = Convert.ToInt32(parametros[0].ToString());
                    int proyecto = Convert.ToInt32(parametros[1].ToString());
                    int local = Convert.ToInt32(parametros[2].ToString());
                    int tipoAlmacen = Convert.ToInt32(parametros[3].ToString());
                    int estado = Convert.ToInt32(parametros[4].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();
                    resul = obj_negocio.get_almacenesCab(delegacion, proyecto, local, tipoAlmacen, estado);

                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_delegaciones(id_usuario);

                    resul = res;
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_locales(id_usuario);

                    resul = res;
                }
                else if (opcion == 4)
                {
                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_tiposAlmacen();

                    resul = res;
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int id_delegacion = Convert.ToInt32(parametros[0].ToString());
                    int id_usuario = Convert.ToInt32(parametros[1].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_proyectosDelegacion(id_delegacion, id_usuario);

                    resul = res;
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');
                    int idTipoAlmacen = Convert.ToInt32(parametros[0].ToString());

                    tbl_Alm_Almacenes objReemplazar;
                    objReemplazar = db.tbl_Alm_Almacenes.Where(u => u.id_Almacen == idTipoAlmacen).FirstOrDefault<tbl_Alm_Almacenes>();
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

        public object Puttbl_Alm_Almacenes(int id, tbl_Alm_Almacenes tbl_Alm_Almacenes)
        {
            Resultado res = new Resultado();

            tbl_Alm_Almacenes objReemplazar;
            objReemplazar = db.tbl_Alm_Almacenes.Where(u => u.id_Almacen == id).FirstOrDefault<tbl_Alm_Almacenes>();

            objReemplazar.id_Empresa = tbl_Alm_Almacenes.id_Empresa;
            objReemplazar.id_Local = tbl_Alm_Almacenes.id_Local;
            objReemplazar.id_Delegacion = tbl_Alm_Almacenes.id_Delegacion;

            objReemplazar.id_TipoAlmacen = tbl_Alm_Almacenes.id_TipoAlmacen;
            objReemplazar.descripcion_Almacen = tbl_Alm_Almacenes.descripcion_Almacen;
            objReemplazar.direccion_Almacen = tbl_Alm_Almacenes.direccion_Almacen;

            objReemplazar.MatNormall_Almacen = tbl_Alm_Almacenes.MatNormall_Almacen;
            objReemplazar.MatUsado_Almacen = tbl_Alm_Almacenes.MatUsado_Almacen;
            objReemplazar.MatBaja_Almacen = tbl_Alm_Almacenes.MatBaja_Almacen;

            objReemplazar.Stock_EmpresObra = tbl_Alm_Almacenes.Stock_EmpresObra;
            objReemplazar.Stock_EmpresPersonal = tbl_Alm_Almacenes.Stock_EmpresPersonal;
            objReemplazar.id_Proyecto = tbl_Alm_Almacenes.id_Proyecto;


            objReemplazar.estado = tbl_Alm_Almacenes.estado;
            objReemplazar.usuario_edicion = tbl_Alm_Almacenes.usuario_creacion;
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

        public object Posttbl_Alm_Almacenes(tbl_Alm_Almacenes tbl_Alm_Almacenes)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Delegacion obj_delegacion = db.tbl_Delegacion.Find(tbl_Alm_Almacenes.id_Delegacion);

                tbl_Alm_Almacenes.id_Empresa = (obj_delegacion == null) ? 0 : obj_delegacion.id_Empresa;
                tbl_Alm_Almacenes.fecha_creacion = DateTime.Now;
                db.tbl_Alm_Almacenes.Add(tbl_Alm_Almacenes);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Alm_Almacenes.id_Almacen;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblAlm_Almacenes/5
        [ResponseType(typeof(tbl_Alm_Almacenes))]
        public IHttpActionResult Deletetbl_Alm_Almacenes(int id)
        {
            tbl_Alm_Almacenes tbl_Alm_Almacenes = db.tbl_Alm_Almacenes.Find(id);
            if (tbl_Alm_Almacenes == null)
            {
                return NotFound();
            }

            db.tbl_Alm_Almacenes.Remove(tbl_Alm_Almacenes);
            db.SaveChanges();

            return Ok(tbl_Alm_Almacenes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Alm_AlmacenesExists(int id)
        {
            return db.tbl_Alm_Almacenes.Count(e => e.id_Almacen == id) > 0;
        }
    }
}