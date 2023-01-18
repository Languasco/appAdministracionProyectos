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
    public class tblObra_TDController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblObra_TD
        public IQueryable<tbl_Obra_TD> Gettbl_Obra_TD()
        {
            return db.tbl_Obra_TD;
        }

        // GET: api/tblObra_TD/5
        [ResponseType(typeof(tbl_Obra_TD))]
        public object Gettbl_Obra_TD(int opcion, string filtro)
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
                    int area = Convert.ToInt32(parametros[2].ToString());
                    int tipoObra = Convert.ToInt32(parametros[3].ToString());
                    int estado = Convert.ToInt32(parametros[4].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL(); 
                    resul = obj_negocio.get_obrasCab(delegacion, proyecto, area, tipoObra, estado); 
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idTipoAlmacen = Convert.ToInt32(parametros[0].ToString());

                    tbl_Obra_TD objReemplazar;
                    objReemplazar = db.tbl_Obra_TD.Where(u => u.id_TD == idTipoAlmacen).FirstOrDefault<tbl_Obra_TD>();
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
                    int idUsuario= Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_estadosSigetramas(idUsuario);

                    resul = res;
                }
                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');
                    int idUsuario = Convert.ToInt32(parametros[0].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();

                    res.ok = true;
                    res.data = obj_negocio.get_clientes(idUsuario);

                    resul = res;
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    string nroObra = parametros[0].ToString().ToUpper();

                    if (db.tbl_Obra_TD.Count(e => e.Codigo_TD.ToUpper() == nroObra) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');
                    int idObra = Convert.ToInt32(parametros[0].ToString());

                    res.ok = true;
                    res.data = (from a in db.tbl_Obra_TD
                                where a.id_TD == idObra
                                select new
                                {
                                   a.id_TD,
                                   a.id_TipoTD,
                                   a.Codigo_TD,
                                   a.id_EstaCliente,
                                   a.descripcion_TD,
                                   a.id_Area,
                                   a.direccion_TD,
                                   a.fechaRecepcion_TD,
                                   a.fechaInicio_TD,
                                   a.fechaTermino_TD,
                                   a.id_Cliente_TD,
                                   a.id_Colaborador_TD,
                                   a.id_Ubigeo,
                                   a.salidaMat_TD,
                                   a.devolucionMat_TD,
                                   a.transferenciaOrigen_TD,
                                   a.transferenciaDestino_TD,
                                   a.estado,
                                   a.usuario_creacion,
                                   a.id_Empresa,
                                   a.id_Delegacion,
                                   a.id_Proyecto,


                                }).ToList();

                    resul = res;
                }
                else if (opcion == 7)
                {
                    string[] parametros = filtro.Split('|');
                    int idObra = Convert.ToInt32(parametros[0].ToString());

                    res.ok = true;
                    res.data = (from a in db.tbl_Obra_TD_Empresa
                                join b in db.tbl_Obra_TD on a.id_TD equals b.id_TD
                                join c in db.tbl_CuentaCorriente on a.id_Colaborador_TD equals c.id_CtaCte

                                where a.id_TD == idObra
                                select new
                                {
                                  a.id_ObraTD_Empresa,
                                  a.id_TD,
                                  a.id_Colaborador_TD,
                                  a.prioridad_TD,
                                  a.estado,
                                  c.nroRUC_CtaCte,
                                  c.razonSocial_CtaCte
                                }).ToList();

                    resul = res;
                }
                else if (opcion == 8)
                {
                    string[] parametros = filtro.Split('|');
                    int idObraTD_Empresa = Convert.ToInt32(parametros[0].ToString());

                    tbl_Obra_TD_Empresa tbl_Obra_TD_Empresa = db.tbl_Obra_TD_Empresa.Find(idObraTD_Empresa);
                    if (tbl_Obra_TD_Empresa == null)
                    {
                        res.ok = false;
                        res.data = "No existe registro con id enviado, intente nuevamente";
                    }
                    else
                    {
                        try
                        {
                            db.tbl_Obra_TD_Empresa.Remove(tbl_Obra_TD_Empresa);
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

        public object Puttbl_Obra_TD(int id, tbl_Obra_TD tbl_Obra_TD)
        {
            Resultado res = new Resultado();

            tbl_Obra_TD objReemplazar;
            objReemplazar = db.tbl_Obra_TD.Where(u => u.id_TD == id).FirstOrDefault<tbl_Obra_TD>();
             
            objReemplazar.id_TipoTD = tbl_Obra_TD.id_TipoTD;
            objReemplazar.Codigo_TD = tbl_Obra_TD.Codigo_TD;

            objReemplazar.id_EstaCliente = tbl_Obra_TD.id_EstaCliente;
            objReemplazar.descripcion_TD = tbl_Obra_TD.descripcion_TD;
            objReemplazar.id_Area = tbl_Obra_TD.id_Area;
            objReemplazar.direccion_TD = tbl_Obra_TD.direccion_TD;

            objReemplazar.fechaRecepcion_TD = tbl_Obra_TD.fechaRecepcion_TD;
            objReemplazar.fechaInicio_TD = tbl_Obra_TD.fechaInicio_TD;
            objReemplazar.fechaTermino_TD = tbl_Obra_TD.fechaTermino_TD;
            objReemplazar.id_Cliente_TD = tbl_Obra_TD.id_Cliente_TD;
            objReemplazar.id_Colaborador_TD = tbl_Obra_TD.id_Colaborador_TD;

            objReemplazar.id_Ubigeo = tbl_Obra_TD.id_Ubigeo;
            objReemplazar.salidaMat_TD = tbl_Obra_TD.salidaMat_TD;
            objReemplazar.devolucionMat_TD = tbl_Obra_TD.devolucionMat_TD;
            objReemplazar.transferenciaOrigen_TD = tbl_Obra_TD.transferenciaOrigen_TD;
            objReemplazar.transferenciaDestino_TD = tbl_Obra_TD.transferenciaDestino_TD;


            objReemplazar.id_Empresa = tbl_Obra_TD.id_Empresa;
            objReemplazar.id_Delegacion = tbl_Obra_TD.id_Delegacion;
            objReemplazar.id_Proyecto = tbl_Obra_TD.id_Proyecto;
            objReemplazar.estado = tbl_Obra_TD.estado;
            objReemplazar.usuario_edicion = tbl_Obra_TD.usuario_creacion;
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

        public object Posttbl_Obra_TD(tbl_Obra_TD tbl_Obra_TD)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Delegacion obj_delegacion = db.tbl_Delegacion.Find(tbl_Obra_TD.id_Delegacion);
                
                tbl_Obra_TD.id_Empresa = (obj_delegacion == null) ? 0 : obj_delegacion.id_Empresa;
                tbl_Obra_TD.fecha_creacion = DateTime.Now;
                db.tbl_Obra_TD.Add(tbl_Obra_TD);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Obra_TD.id_TD;

            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblObra_TD/5
        [ResponseType(typeof(tbl_Obra_TD))]
        public IHttpActionResult Deletetbl_Obra_TD(int id)
        {
            tbl_Obra_TD tbl_Obra_TD = db.tbl_Obra_TD.Find(id);
            if (tbl_Obra_TD == null)
            {
                return NotFound();
            }

            db.tbl_Obra_TD.Remove(tbl_Obra_TD);
            db.SaveChanges();

            return Ok(tbl_Obra_TD);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Obra_TDExists(int id)
        {
            return db.tbl_Obra_TD.Count(e => e.id_TD == id) > 0;
        }
    }
}