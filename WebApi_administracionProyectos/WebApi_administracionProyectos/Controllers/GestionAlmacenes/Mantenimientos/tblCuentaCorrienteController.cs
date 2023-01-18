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
    public class tblCuentaCorrienteController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblCuentaCorriente
        public IQueryable<tbl_CuentaCorriente> Gettbl_CuentaCorriente()
        {
            return db.tbl_CuentaCorriente;
        }
 
        // GET: api/tblAlm_Unidades_Medida/5
        [ResponseType(typeof(tbl_CuentaCorriente))]
        public object Gettbl_CuentaCorriente(int opcion, string filtro)
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
                    res.data = obj_negocio.get_cuentaCorrienteCab(idEstado);

                    resul = res;
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idCtaCte = Convert.ToInt32(parametros[0].ToString());

                    tbl_CuentaCorriente objReemplazar;
                    objReemplazar = db.tbl_CuentaCorriente.Where(u => u.id_CtaCte == idCtaCte).FirstOrDefault<tbl_CuentaCorriente>();
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
                    string nroRuc = parametros[0].ToString().ToUpper();

                    if (db.tbl_CuentaCorriente.Count(e => e.nroRUC_CtaCte.ToUpper() == nroRuc) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
                }
                else if (opcion == 4)
                {

                    res.ok = true;
                    res.data = (from a in db.tbl_Areas
                                where a.estado == 1
                                select new
                                {
                                    id = a.id_Area,
                                    descripcion = a.nombre_area,
                                    a.estado
                                }).ToList().OrderBy(e => e.descripcion);

                    resul = res;
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int idEstado = Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_cuentaCorrienteCab(idEstado);

                    resul = res;
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');
                    int idCuentaCorriente = Convert.ToInt32(parametros[0].ToString());

                    res.ok = true;
                    res.data = (from a in db.tbl_CuentaCorriente_Servicio
                                join b in db.tbl_Areas  on a.id_Area equals b.id_Area
                                where a.estado == 1 && a.id_CtaCte == idCuentaCorriente
                                select new
                                {
                                   a.id_CtaCte_Servicio,
                                   a.id_CtaCte,
                                   a.id_Area,
                                   b.descripcion_area,
                                   a.prioridad_CtaCte_Servicio,
                                   a.estado
                                }).ToList();

                    resul = res;
                }
                else if (opcion ==7)
                {
                    string[] parametros = filtro.Split('|');
                    int id_CtaCte_Servicio = Convert.ToInt32(parametros[0].ToString());

                    tbl_CuentaCorriente_Servicio tbl_CuentaCorriente_Servicio = db.tbl_CuentaCorriente_Servicio.Find(id_CtaCte_Servicio);
                    if (tbl_CuentaCorriente_Servicio == null)
                    {
                        res.ok = false;
                        res.data = "No existe registro con id enviado, intente nuevamente";
                    }
                    else {
                        try
                        {
                            db.tbl_CuentaCorriente_Servicio.Remove(tbl_CuentaCorriente_Servicio);
                            db.SaveChanges();

                            res.ok = true;
                            res.data = "OK";
                        }
                        catch (Exception ex)
                        {
                            res.ok = false;
                            res.data = ex.Message;
                        }
                    }

                    resul = res;
                }
                else if (opcion == 8)
                {

                    res.ok = true;
                    res.data = (from a in db.tbl_Tipo_TD
                                where a.estado == 1
                                select new
                                {
                                    id = a.id_TipoObra,
                                    descripcion = a.abreviatura_TipoObra,
                                    a.estado
                                }).ToList().OrderBy(e => e.descripcion);

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

        public object Puttbl_CuentaCorriente(int id, tbl_CuentaCorriente tbl_CuentaCorriente)
        {
            Resultado res = new Resultado();

            tbl_CuentaCorriente objReemplazar;
            objReemplazar = db.tbl_CuentaCorriente.Where(u => u.id_CtaCte == id).FirstOrDefault<tbl_CuentaCorriente>();
 
            objReemplazar.nroRUC_CtaCte = tbl_CuentaCorriente.nroRUC_CtaCte;
            objReemplazar.tipoPersona_CtaCte = tbl_CuentaCorriente.tipoPersona_CtaCte;
            objReemplazar.razonSocial_CtaCte = tbl_CuentaCorriente.razonSocial_CtaCte;

            objReemplazar.direccion_CtaCte = tbl_CuentaCorriente.direccion_CtaCte;
            objReemplazar.telefono1_CtaCte = tbl_CuentaCorriente.telefono1_CtaCte;
            objReemplazar.telefono2_CtaCte = tbl_CuentaCorriente.telefono2_CtaCte;
            objReemplazar.paginaWeb_CtaCte = tbl_CuentaCorriente.paginaWeb_CtaCte;

            objReemplazar.contacto_CtaCte = tbl_CuentaCorriente.contacto_CtaCte;
            objReemplazar.email_CtaCte = tbl_CuentaCorriente.email_CtaCte;
            objReemplazar.proveedor_CtaCte = tbl_CuentaCorriente.proveedor_CtaCte;
            objReemplazar.cliente_CtaCte = tbl_CuentaCorriente.cliente_CtaCte;

            objReemplazar.transportista = tbl_CuentaCorriente.transportista;
            objReemplazar.colaborador_CtaCte = tbl_CuentaCorriente.colaborador_CtaCte;
            objReemplazar.salidaMat_CtaCte = tbl_CuentaCorriente.salidaMat_CtaCte;

            objReemplazar.devolucionMat_CtaCte = tbl_CuentaCorriente.devolucionMat_CtaCte;
            objReemplazar.transferenciaOrigen_CtaCte = tbl_CuentaCorriente.transferenciaOrigen_CtaCte;
            objReemplazar.transferenciaDestino_CtaCte = tbl_CuentaCorriente.transferenciaDestino_CtaCte;
 
            objReemplazar.estado = tbl_CuentaCorriente.estado;
            objReemplazar.usuario_edicion = tbl_CuentaCorriente.usuario_creacion;
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

        public object Posttbl_CuentaCorriente(tbl_CuentaCorriente tbl_CuentaCorriente)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_CuentaCorriente.fecha_creacion = DateTime.Now;
                db.tbl_CuentaCorriente.Add(tbl_CuentaCorriente);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_CuentaCorriente.id_CtaCte;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblCuentaCorriente/5
        [ResponseType(typeof(tbl_CuentaCorriente))]
        public IHttpActionResult Deletetbl_CuentaCorriente(int id)
        {
            tbl_CuentaCorriente tbl_CuentaCorriente = db.tbl_CuentaCorriente.Find(id);
            if (tbl_CuentaCorriente == null)
            {
                return NotFound();
            }

            db.tbl_CuentaCorriente.Remove(tbl_CuentaCorriente);
            db.SaveChanges();

            return Ok(tbl_CuentaCorriente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_CuentaCorrienteExists(int id)
        {
            return db.tbl_CuentaCorriente.Count(e => e.id_CtaCte == id) > 0;
        }
    }
}