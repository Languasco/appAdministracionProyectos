
using Entidades;
using Negocio.GestionAlmacenes.Mantenimientos;
using Negocio.Resultados;
using Negocio.Uploads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_administracionProyectos.Controllers.Upload
{
    [EnableCors("*", "*", "*")]
    public class UploadsController : ApiController
    {
        private GestionProyectosEntities db = new GestionProyectosEntities();

        [HttpPost]
        [Route("api/Uploads/post_imagenPersonal")]
        public object post_imagenPersonal(string filtros)
        {
            Resultado res = new Resultado();
            string nombreFile = "";
            string nombreFileServer = "";
            string path = "";
            string url = ConfigurationManager.AppSettings["imagen"];

            try
            {
                var file = HttpContext.Current.Request.Files["file"];
                string extension = System.IO.Path.GetExtension(file.FileName);

                string[] parametros = filtros.Split('|');
                int idPersonal = Convert.ToInt32(parametros[0].ToString());
                int idusuarioLogin = Convert.ToInt32(parametros[1].ToString());

                nombreFile = file.FileName;

                //-----generando clave unica---
                var guid = Guid.NewGuid();
                var guidB = guid.ToString("B");
                nombreFileServer = idPersonal + "_image_person_" + Guid.Parse(guidB) + extension;

                //---almacenando la imagen--
                path = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Imagen/" + nombreFileServer);
                file.SaveAs(path);


                //------suspendemos el hilo, y esperamos ..
                System.Threading.Thread.Sleep(1000);

                if (File.Exists(path))
                {
                    ///----validando que en servidor solo halla una sola foto---
                    tbl_Personal object_usuario;
                    object_usuario = db.tbl_Personal.Where(p => p.id_Personal == idPersonal).FirstOrDefault<tbl_Personal>();
                    string urlFotoAntes = (string.IsNullOrEmpty(object_usuario.fotoBase64)) ? "" : object_usuario.fotoBase64;

                    MantenimientoAlmacen_BL obj_negocio = new MantenimientoAlmacen_BL();
                    obj_negocio.Set_Actualizar_imagenPersonal(idPersonal, nombreFileServer);

                    res.ok = true;
                    res.data = url + nombreFileServer;

                    //---si previamente habia una foto, al reemplazarla borramos la anterior
                    if (urlFotoAntes.Length > 0)
                    {
                        path = System.Web.Hosting.HostingEnvironment.MapPath("~/Archivos/Imagen/" + urlFotoAntes);

                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                    }
                }
                else
                {
                    res.ok = false;
                    res.data = "No se pudo guardar el archivo en el servidor..";
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }

            return res;
        }



    }
}
