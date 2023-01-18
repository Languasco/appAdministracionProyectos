using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class tblPersonalController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        // GET: api/tblPersonal
        public IQueryable<tbl_Personal> Gettbl_Personal()
        {
            return db.tbl_Personal;
        }

        // GET: api/tblPersonal/5
        [ResponseType(typeof(tbl_Personal))]
        public object Gettbl_Personal(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idDelegacion = Convert.ToInt32(parametros[0].ToString());
                    int idProyecto = Convert.ToInt32(parametros[1].ToString());
                    string personal =  parametros[2].ToString();
                    int idEstado = Convert.ToInt32(parametros[3].ToString());
                    int idUsuario = Convert.ToInt32(parametros[4].ToString());

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();               
                    resul = obj_negocio.get_personalCab(idDelegacion, idProyecto, personal, idEstado,  idUsuario); 
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int idPersonal = Convert.ToInt32(parametros[0].ToString());

                    tbl_Personal objReemplazar;
                    objReemplazar = db.tbl_Personal.Where(u => u.id_Personal == idPersonal).FirstOrDefault<tbl_Personal>();
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
                    string nroDoc  = parametros[0].ToString().Trim();

                    if (db.tbl_Personal.Count(e => e.nroDoc_Personal == nroDoc) > 0)
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
                    res.data = (from a in db.tbl_Cargo_Personal
                                where a.estado == 1
                                select new
                                {
                                   a.id_Cargo,
                                   a.nombre_cargo
                                }).ToList().OrderBy(c => c.nombre_cargo);
                    resul = res;
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int idPersonal = Convert.ToInt32(parametros[0].ToString());

                    string url = ConfigurationManager.AppSettings["imagen"];

                    res.ok = true;
                    res.data = (from a in db.tbl_Personal
                                where a.id_Personal == idPersonal
                                select new
                                {
                                      a.id_Personal,
                                      a.nroDoc_Personal,
                                      a.tipoDoc_Personal,
                                      a.apellidos_Personal,
                                      a.nombres_Personal,
                                      a.direccion_Personal,
                                      a.telefono_Personal,
                                      a.costoMo_Personal,
                                      a.fechaIngreso_Personal,
                                      a.id_Cargo,
                                      a.tipoPersonal,
                                      a.fechaCese_Personal,
                                      a.retiraMate_Personal,
                                      a.retiraEquipamiento_Personal,
                                      a.estado,
                                      a.usuario_creacion,
                                      a.fecha_creacion,
                                      a.usuario_edicion,
                                      a.fecha_edicion,
                                    fotoBase64 = (a.fotoBase64 == null || a.fotoBase64 == "") ? "" : url +  a.fotoBase64,
                                      a.id_Empresa,
                                      a.id_Delegacion,
                                      a.id_Proyecto
                                }).ToList();

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

        public object Puttbl_Personal(int id, tbl_Personal tbl_Personal)
        {
            Resultado res = new Resultado();

            tbl_Personal objReemplazar;
            objReemplazar = db.tbl_Personal.Where(u => u.id_Personal == id).FirstOrDefault<tbl_Personal>();
                         
            objReemplazar.nroDoc_Personal = tbl_Personal.nroDoc_Personal;
            objReemplazar.tipoDoc_Personal = tbl_Personal.tipoDoc_Personal;
            objReemplazar.apellidos_Personal = tbl_Personal.apellidos_Personal;

            objReemplazar.nombres_Personal = tbl_Personal.nombres_Personal;
            objReemplazar.direccion_Personal = tbl_Personal.direccion_Personal;
            objReemplazar.telefono_Personal = tbl_Personal.telefono_Personal;
            objReemplazar.costoMo_Personal = tbl_Personal.costoMo_Personal;
            objReemplazar.fechaIngreso_Personal = tbl_Personal.fechaIngreso_Personal;

            objReemplazar.id_Cargo = tbl_Personal.id_Cargo;
            objReemplazar.tipoPersonal = tbl_Personal.tipoPersonal;
            objReemplazar.fechaCese_Personal = tbl_Personal.fechaCese_Personal;
            objReemplazar.retiraMate_Personal = tbl_Personal.retiraMate_Personal;
            objReemplazar.retiraEquipamiento_Personal = tbl_Personal.retiraEquipamiento_Personal; 
            objReemplazar.id_Delegacion = tbl_Personal.id_Delegacion;
            objReemplazar.id_Proyecto = tbl_Personal.id_Proyecto;

            objReemplazar.estado = tbl_Personal.estado;
            objReemplazar.usuario_edicion = tbl_Personal.usuario_creacion;
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

        public object Posttbl_Personal(tbl_Personal tbl_Personal)
        {
            Resultado res = new Resultado();
            try
            {
                tbl_Delegacion obj_delegacion = db.tbl_Delegacion.Find(tbl_Personal.id_Delegacion);

                tbl_Personal.id_Empresa = (obj_delegacion == null) ? 0 : obj_delegacion.id_Empresa;
                tbl_Personal.fecha_creacion = DateTime.Now;
                db.tbl_Personal.Add(tbl_Personal);
                db.SaveChanges();

                res.ok = true;
                res.data = tbl_Personal.id_Personal;
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.InnerException.Message;

            }
            return res;
        }

        // DELETE: api/tblPersonal/5
        [ResponseType(typeof(tbl_Personal))]
        public IHttpActionResult Deletetbl_Personal(int id)
        {
            tbl_Personal tbl_Personal = db.tbl_Personal.Find(id);
            if (tbl_Personal == null)
            {
                return NotFound();
            }

            db.tbl_Personal.Remove(tbl_Personal);
            db.SaveChanges();

            return Ok(tbl_Personal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PersonalExists(int id)
        {
            return db.tbl_Personal.Count(e => e.id_Personal == id) > 0;
        }
    }
}