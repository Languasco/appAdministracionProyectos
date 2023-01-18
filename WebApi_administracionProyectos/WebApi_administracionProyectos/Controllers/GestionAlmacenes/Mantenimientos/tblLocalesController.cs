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
    public class tblLocalesController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblLocales
        public IQueryable<tbl_Locales> Gettbl_Locales()
        {
            return db.tbl_Locales;
        }

        // GET: api/tblLocales/5
        [ResponseType(typeof(tbl_Locales))]
        public object Gettbl_Locales(int opcion, string filtro)
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
                    res.data = obj_negocio.get_localesCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {

                    res.ok = true;
                    res.data = (from a in db.tbl_Empresas
                                where a.estado == 1
                                select new
                                {
                                   id= a.id_Empresa,
                                   descripcion =  a.razonsocial_empresa,
                                   a.estado
                                }).ToList().OrderBy(e => e.descripcion);

                    resul = res;
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int idLocal = Convert.ToInt32(parametros[0].ToString());

                    tbl_Locales objReemplazar;
                    objReemplazar = db.tbl_Locales.Where(u => u.Id_Local == idLocal).FirstOrDefault<tbl_Locales>();
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


        public object Puttbl_Locales(int id, tbl_Locales tbl_Locales)
        {
            Resultado res = new Resultado();


            tbl_Locales objReemplazar;
            objReemplazar = db.tbl_Locales.Where(u => u.Id_Local == id).FirstOrDefault<tbl_Locales>();

            objReemplazar.Id_Empresa = tbl_Locales.Id_Empresa;
            objReemplazar.nombre_local = tbl_Locales.nombre_local;
            objReemplazar.direccion_local = tbl_Locales.direccion_local;
            objReemplazar.Id_Ubicacion = tbl_Locales.Id_Ubicacion;
            objReemplazar.orden_local = tbl_Locales.orden_local;

            objReemplazar.estado = tbl_Locales.estado; 
            objReemplazar.usuario_edicion = tbl_Locales.usuario_creacion;
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

        public object Posttbl_Locales(tbl_Locales tbl_Locales)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Locales.fecha_creacion = DateTime.Now;
                db.tbl_Locales.Add(tbl_Locales);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Locales.Id_Local;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }



        // DELETE: api/tblLocales/5
        [ResponseType(typeof(tbl_Locales))]
        public IHttpActionResult Deletetbl_Locales(int id)
        {
            tbl_Locales tbl_Locales = db.tbl_Locales.Find(id);
            if (tbl_Locales == null)
            {
                return NotFound();
            }

            db.tbl_Locales.Remove(tbl_Locales);
            db.SaveChanges();

            return Ok(tbl_Locales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_LocalesExists(int id)
        {
            return db.tbl_Locales.Count(e => e.Id_Local == id) > 0;
        }
    }
}