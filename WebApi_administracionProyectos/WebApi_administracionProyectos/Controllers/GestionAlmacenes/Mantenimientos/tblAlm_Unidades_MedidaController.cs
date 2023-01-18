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
    public class tblAlm_Unidades_MedidaController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblAlm_Unidades_Medida
        public IQueryable<tbl_Alm_Unidades_Medida> Gettbl_Alm_Unidades_Medida()
        {
            return db.tbl_Alm_Unidades_Medida;
        }

        // GET: api/tblAlm_Unidades_Medida/5
        [ResponseType(typeof(tbl_Alm_Unidades_Medida))]
        public object Gettbl_Alm_Unidades_Medida(int opcion, string filtro)
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
                    res.data = obj_negocio.get_unidadMedidasCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idUnidadMedida = Convert.ToInt32(parametros[0].ToString());

                    tbl_Alm_Unidades_Medida objReemplazar;
                    objReemplazar = db.tbl_Alm_Unidades_Medida.Where(u => u.id_UnidadMedida == idUnidadMedida).FirstOrDefault<tbl_Alm_Unidades_Medida>();
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

        public object Puttbl_Alm_Unidades_Medida(int id, tbl_Alm_Unidades_Medida tbl_Alm_Unidades_Medida)
        {
            Resultado res = new Resultado();

            tbl_Alm_Unidades_Medida objReemplazar;
            objReemplazar = db.tbl_Alm_Unidades_Medida.Where(u => u.id_UnidadMedida == id).FirstOrDefault<tbl_Alm_Unidades_Medida>();

            objReemplazar.nombre_UnidadMedida = tbl_Alm_Unidades_Medida.nombre_UnidadMedida;
            objReemplazar.abreviatura_UnidadMedida = tbl_Alm_Unidades_Medida.abreviatura_UnidadMedida;
            objReemplazar.codigo_Sunat = tbl_Alm_Unidades_Medida.codigo_Sunat;

            objReemplazar.estado = tbl_Alm_Unidades_Medida.estado;
            objReemplazar.usuario_edicion = tbl_Alm_Unidades_Medida.usuario_creacion;
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

        public object Posttbl_Alm_Unidades_Medida(tbl_Alm_Unidades_Medida tbl_Alm_Unidades_Medida)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Alm_Unidades_Medida.fecha_creacion = DateTime.Now;
                db.tbl_Alm_Unidades_Medida.Add(tbl_Alm_Unidades_Medida);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Alm_Unidades_Medida.id_UnidadMedida;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblAlm_Unidades_Medida/5
        [ResponseType(typeof(tbl_Alm_Unidades_Medida))]
        public IHttpActionResult Deletetbl_Alm_Unidades_Medida(int id)
        {
            tbl_Alm_Unidades_Medida tbl_Alm_Unidades_Medida = db.tbl_Alm_Unidades_Medida.Find(id);
            if (tbl_Alm_Unidades_Medida == null)
            {
                return NotFound();
            }

            db.tbl_Alm_Unidades_Medida.Remove(tbl_Alm_Unidades_Medida);
            db.SaveChanges();

            return Ok(tbl_Alm_Unidades_Medida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Alm_Unidades_MedidaExists(int id)
        {
            return db.tbl_Alm_Unidades_Medida.Count(e => e.id_UnidadMedida == id) > 0;
        }
    }
}