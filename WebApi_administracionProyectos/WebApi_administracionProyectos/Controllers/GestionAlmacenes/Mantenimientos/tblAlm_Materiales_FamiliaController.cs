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
    public class tblAlm_Materiales_FamiliaController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAlm_Materiales_Familia
        public IQueryable<tbl_Alm_Materiales_Familia> Gettbl_Alm_Materiales_Familia()
        {
            return db.tbl_Alm_Materiales_Familia;
        }

        // GET: api/tblAlm_Materiales_Familia/5
        [ResponseType(typeof(tbl_Alm_Materiales_Familia))]
        // GET: api/tblAlm_Unidades_Medida/5
        public object Gettbl_Alm_Materiales_Familia(int opcion, string filtro)
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
                    res.data = obj_negocio.get_familiasCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idFamiliaMaterial = Convert.ToInt32(parametros[0].ToString());

                    tbl_Alm_Materiales_Familia objReemplazar;
                    objReemplazar = db.tbl_Alm_Materiales_Familia.Where(u => u.id_FamiliaMaterial == idFamiliaMaterial).FirstOrDefault<tbl_Alm_Materiales_Familia>();
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

        public object Puttbl_Alm_Materiales_Familia(int id, tbl_Alm_Materiales_Familia tbl_Alm_Materiales_Familia)
        {
            Resultado res = new Resultado();

            tbl_Alm_Materiales_Familia objReemplazar;
            objReemplazar = db.tbl_Alm_Materiales_Familia.Where(u => u.id_FamiliaMaterial == id).FirstOrDefault<tbl_Alm_Materiales_Familia>();

            objReemplazar.nombre_FamiliaMaterial = tbl_Alm_Materiales_Familia.nombre_FamiliaMaterial;
            objReemplazar.abreviatura_FamiliaMaterial = tbl_Alm_Materiales_Familia.abreviatura_FamiliaMaterial; 

            objReemplazar.estado = tbl_Alm_Materiales_Familia.estado;
            objReemplazar.usuario_edicion = tbl_Alm_Materiales_Familia.usuario_creacion;
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

        public object Posttbl_Alm_Materiales_Familia(tbl_Alm_Materiales_Familia tbl_Alm_Materiales_Familia)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Alm_Materiales_Familia.fecha_creacion = DateTime.Now;
                db.tbl_Alm_Materiales_Familia.Add(tbl_Alm_Materiales_Familia);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Alm_Materiales_Familia.id_FamiliaMaterial;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblAlm_Materiales_Familia/5
        [ResponseType(typeof(tbl_Alm_Materiales_Familia))]
        public IHttpActionResult Deletetbl_Alm_Materiales_Familia(int id)
        {
            tbl_Alm_Materiales_Familia tbl_Alm_Materiales_Familia = db.tbl_Alm_Materiales_Familia.Find(id);
            if (tbl_Alm_Materiales_Familia == null)
            {
                return NotFound();
            }

            db.tbl_Alm_Materiales_Familia.Remove(tbl_Alm_Materiales_Familia);
            db.SaveChanges();

            return Ok(tbl_Alm_Materiales_Familia);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Alm_Materiales_FamiliaExists(int id)
        {
            return db.tbl_Alm_Materiales_Familia.Count(e => e.id_FamiliaMaterial == id) > 0;
        }
    }
}