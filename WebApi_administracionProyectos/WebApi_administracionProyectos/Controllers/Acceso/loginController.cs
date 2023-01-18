 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Entidades;
using Entidades.Acceso;
using Microsoft.VisualBasic;
using Negocio.Resultados;

namespace WebApi_administracionProyectos.Controllers.Acceso
{
    [EnableCors("*", "*", "*")]
    public class loginController : ApiController
    {
        private   GestionProyectosEntities db = new GestionProyectosEntities();

        public object GetLogin(int opcion, string filtro)
        {
            Resultado res = new Resultado();
            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    string login = parametros[0].ToString();
                    string contra = parametros[1].ToString();
          

                    var flagLogin = db.tbl_Usuarios.Count(e => e.login_usuario == login && e.contrasenia_usuario == contra);

                    if (flagLogin == 0)
                    {
                        res.ok = false;
                        res.data = "El usuario y/o contraseña no son correctos, verifique ";
 
                        resul = res;
                    }
                    else
                    {
                        Menu listamenu = new Menu();
                        tbl_Usuarios objUsuario = db.tbl_Usuarios.Where(p => p.login_usuario == login && p.contrasenia_usuario == contra).SingleOrDefault();

                        List<MenuPermisos> listaAccesos = new List<MenuPermisos>();

                        var Parents = new string[] { "0" };

                        var listaModulos = (from w in db.tbl_Web_Aceesos
                                            join od in db.tbl_Definicion_Opciones on w.id_Opcion equals od.id_Opcion
                                         join u in db.tbl_Usuarios on w.id_Usuario equals u.id_Usuario
                                         where u.id_Usuario == objUsuario.id_Usuario && Parents.Contains(od.parentID.ToString()) && od.estado == 1
                                         orderby od.orden_Opcion ascending
                                         select new
                                         {
                                             id_opcion = w.id_Opcion,
                                             id_usuarios = w.id_Usuario,
                                             nombre_principal = od.nombre_opcion,
                                             parent_id_principal = od.parentID,
                                             urlmagene_principal = od.urlImagen_Opcion


                                         }).Distinct();

                        foreach (var item in listaModulos)
                        {
                            MenuPermisos listaJsonObj = new MenuPermisos();

                            listaJsonObj.id_opcion = Convert.ToInt32(item.id_opcion);
                            listaJsonObj.id_usuarios = Convert.ToInt32(item.id_usuarios);
                            listaJsonObj.nombre_principal = item.nombre_principal;
                            listaJsonObj.parent_id_principal = Convert.ToInt32(item.parent_id_principal);
                            listaJsonObj.urlmagene_principal = item.urlmagene_principal;
                            listaJsonObj.listMenu = (from w in db.tbl_Web_Aceesos
                                                     join od in db.tbl_Definicion_Opciones on w.id_Opcion equals od.id_Opcion
                                                     join u in db.tbl_Usuarios on w.id_Usuario equals u.id_Usuario
                                                     where u.id_Usuario == objUsuario.id_Usuario && od.parentID == item.id_opcion && od.estado == 1  ///---&& od.TipoInterface == "W"
                                                     orderby od.orden_Opcion ascending
                                                     select new
                                                     {
                                                         nombre_page = od.nombre_opcion,
                                                         url_page = od.url_opcion,
                                                         orden = od.orden_Opcion,
                                                         id_opcion = od.id_Opcion,
                                                         listMenuItem = (from w3 in db.tbl_Web_Aceesos
                                                                         join od3 in db.tbl_Definicion_Opciones on w3.id_Opcion equals od3.id_Opcion
                                                                         join u3 in db.tbl_Usuarios on w3.id_Usuario equals u3.id_Usuario
                                                                         where u3.id_Usuario == objUsuario.id_Usuario && od3.parentID == od.id_Opcion && od3.estado == 1  ///---&& od.TipoInterface == "W"
                                                                         orderby od3.orden_Opcion ascending
                                                                         select new
                                                                         {
                                                                             nombre_page = od3.nombre_opcion,
                                                                             url_page = od3.url_opcion,
                                                                             orden = od3.orden_Opcion,
                                                                             od3.id_Opcion
                                                                         })
                                                                        .ToList()
                                                                        .Distinct()
                                                     })
                                            .ToList()
                                            .Distinct();

                            listaAccesos.Add(listaJsonObj);
                        }
                                                                                                                                      
                        listamenu.menuPermisos = listaAccesos;
                        //listamenu.menuEventos = get_AccesoEventos(objUsuario.Pub_Usua_Codigo);
                        listamenu.id_usuario = objUsuario.id_Usuario;
                        listamenu.nombre_usuario = objUsuario.apellidos_usuario +  " "+ objUsuario.nombres_usuario;
                        listamenu.id_proveedor = 0 ;
                        listamenu.observacion_usuario = "";
                        res.ok = true;
                        res.data = listamenu; 

                        resul = res;
                    }
                }
 
                else
                {
                    resul = "Opcion seleccionada invalida";
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

        public DataTable get_AccesoEventos(string  idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(Negocio.Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ACCESOS_EVENTOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
                return dt_detalle;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static string EncriptarClave(string cExpresion, bool bEncriptarCadena)
        {
            string cResult = "";
            string cPatron = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwwyz";
            string cEncrip = "^çºªæÆöûÿø£Ø×ƒ¬½¼¡«»ÄÅÉêèï7485912360^çºªæÆöûÿø£Ø×ƒ¬½¼¡«»ÄÅÉêèï";


            if (bEncriptarCadena == true)
            {
                cResult = CHRTRAN(cExpresion, cPatron, cEncrip);
            }
            else
            {
                cResult = CHRTRAN(cExpresion, cEncrip, cPatron);
            }

            return cResult;

        }
        
        public static string CHRTRAN(string cExpresion, string cPatronBase, string cPatronReemplazo)
        {
            string cResult = "";

            int rgChar;
            int nPosReplace;

            for (rgChar = 1; rgChar <= Strings.Len(cExpresion); rgChar++)
            {
                nPosReplace = Strings.InStr(1, cPatronBase, Strings.Mid(cExpresion, rgChar, 1));

                if (nPosReplace == 0)
                {
                    nPosReplace = rgChar;
                    cResult = cResult + Strings.Mid(cExpresion, nPosReplace, 1);
                }
                else
                {
                    if (nPosReplace > cPatronReemplazo.Length)
                    {
                        nPosReplace = rgChar;
                        cResult = cResult + Strings.Mid(cExpresion, nPosReplace, 1);
                    }
                    else
                    {
                        cResult = cResult + Strings.Mid(cPatronReemplazo, nPosReplace, 1);
                    }
                }
            }
            return cResult;
        }

    }
}
