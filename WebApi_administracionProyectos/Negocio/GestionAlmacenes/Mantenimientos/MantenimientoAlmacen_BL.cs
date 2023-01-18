using Entidades.GestionAlmacenes.Mantenimientos;
using Negocio.Conexion;
using Negocio.Resultados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.GestionAlmacenes.Mantenimientos
{
    public class MantenimientoAlmacen_BL
    {

        public DataTable get_localesCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_LOCALES_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_delegacionesCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_DELEGACIONES_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_movimientosAlmacenCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_MOV_ALMACENES_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_tiposAlmacenCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_TIPOS_ALMACEN_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_delegaciones(int id_usuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_DELEGACION_INDIVIDUAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_locales(int id_usuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_LOCALES_INDIVIDUAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        public DataTable get_tiposAlmacen()
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_TIPO_ALMACEN", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
 

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_proyectosDelegacion( int idDelegacion, int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PROYECTO_INDIVIDUAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = idDelegacion;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public object get_almacenesCab(int delegacion, int proyecto, int local, int tipoAlmacen, int estado)
        {
            Resultado res = new Resultado();
            List<Almacenes_E> obj_List = new List<Almacenes_E>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_ALMACEN_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@delegacion", SqlDbType.Int).Value = delegacion;
                        cmd.Parameters.Add("@proyecto", SqlDbType.Int).Value = proyecto;
                        cmd.Parameters.Add("@local", SqlDbType.Int).Value = local;
                        cmd.Parameters.Add("@tipoAlmacen", SqlDbType.Int).Value = tipoAlmacen;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Almacenes_E Entidad = new Almacenes_E();

                                Entidad.id_Almacen = Convert.ToInt32(dr["id_Almacen"]);
                                Entidad.id_Empresa = dr["id_Empresa"].ToString();
                                Entidad.id_Local = dr["id_Local"].ToString();
                                Entidad.id_Delegacion = dr["id_Delegacion"].ToString();

                                Entidad.id_TipoAlmacen = dr["id_TipoAlmacen"].ToString();
                                Entidad.descripcion_Almacen = dr["descripcion_Almacen"].ToString();
                                Entidad.direccion_Almacen = dr["direccion_Almacen"].ToString();

                                Entidad.MatNormall_Almacen = dr["MatNormall_Almacen"].ToString();
                                Entidad.MatUsado_Almacen = dr["MatUsado_Almacen"].ToString();
                                Entidad.MatBaja_Almacen = dr["MatBaja_Almacen"].ToString();
                                Entidad.Stock_EmpresObra = dr["Stock_EmpresObra"].ToString();
                                Entidad.Stock_EmpresPersonal = dr["Stock_EmpresPersonal"].ToString();

                                Entidad.estado = dr["estado"].ToString();
                                Entidad.ruc_empresa = dr["ruc_empresa"].ToString();
                                Entidad.nombre_local = dr["nombre_local"].ToString();
                                Entidad.nombre_delegacion = dr["nombre_delegacion"].ToString();

                                Entidad.codigo_delegacion = dr["codigo_delegacion"].ToString();
                                Entidad.nombre_TipoAlmacen = dr["nombre_TipoAlmacen"].ToString();
                                Entidad.id_Proyecto = dr["id_Proyecto"].ToString();
                                Entidad.nombre_proyecto = dr["nombre_proyecto"].ToString();

                                obj_List.Add(Entidad);
                            }

                            dr.Close();

                            res.ok = true;
                            res.data = obj_List;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public DataTable get_unidadMedidasCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_UNIDAD_MEDIDAS_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }
       
        public DataTable get_familiasCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_FAMILIAS_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_cuentaCorrienteCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_CUENTA_CORRIENTE_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_areasCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_AREAS_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public object get_obrasCab(int delegacion, int  proyecto, int area, int tipoObra, int estado)
        {
            Resultado res = new Resultado();
            List<Obras_E> obj_List = new List<Obras_E>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_OBRAS_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@delegacion", SqlDbType.Int).Value = delegacion;
                        cmd.Parameters.Add("@proyecto", SqlDbType.Int).Value = proyecto;
                        cmd.Parameters.Add("@area", SqlDbType.Int).Value = area;
                        cmd.Parameters.Add("@tipoObra", SqlDbType.Int).Value = tipoObra;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Obras_E Entidad = new Obras_E();

                                Entidad.id_TD = Convert.ToInt32(dr["id_TD"]);
                                Entidad.descripcion_TipoObra = dr["descripcion_TipoObra"].ToString();
                                Entidad.Codigo_TD = dr["Codigo_TD"].ToString();
                                Entidad.id_EstaCliente = dr["id_EstaCliente"].ToString();
                                Entidad.descripcion_TD = dr["descripcion_TD"].ToString();
                                Entidad.nombre_area = dr["nombre_area"].ToString();
                                Entidad.direccion_TD = dr["direccion_TD"].ToString();
                                Entidad.estado = dr["estado"].ToString();

                                obj_List.Add(Entidad);
                            }

                            dr.Close();

                            res.ok = true;
                            res.data = obj_List;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

        public DataTable get_estadosSigetramas(int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_OBRA_ESTADO_SIGETRAMA_COMBO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        public DataTable get_clientes(int idUsuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_OBRA_CLIENTES_COMBO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }

        public DataTable get_cargosCab(int idEstado)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_CARGOS_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt_detalle;
        }


        public object get_personalCab(int idDelegacion,int  idProyecto, string personal, int idEstado , int idUsuario)
        {
            Resultado res = new Resultado();
            List<Personal_E> obj_List = new List<Personal_E>();

            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_PERSONAL_CAB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idDelegacion", SqlDbType.Int).Value = idDelegacion;
                        cmd.Parameters.Add("@idProyecto", SqlDbType.Int).Value = idProyecto;             
                        cmd.Parameters.Add("@personal", SqlDbType.VarChar).Value = personal;
                        cmd.Parameters.Add("@idEstado", SqlDbType.Int).Value = idEstado;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Personal_E Entidad = new Personal_E();
 
                                Entidad.id_Personal = Convert.ToInt32(dr["id_Personal"]);
                                Entidad.nroDoc_Personal = dr["nroDoc_Personal"].ToString();
                                Entidad.apellidos_Personal = dr["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = dr["nombres_Personal"].ToString();
                                Entidad.telefono_Personal = dr["telefono_Personal"].ToString();

                                Entidad.direccion_Personal = dr["direccion_Personal"].ToString();
                                Entidad.fechaIngreso_Personal = dr["fechaIngreso_Personal"].ToString();
                                Entidad.fechaCese_Personal = dr["fechaCese_Personal"].ToString();
                                Entidad.estado = dr["estado"].ToString();

                                obj_List.Add(Entidad);
                            }

                            dr.Close();

                            res.ok = true;
                            res.data = obj_List;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }


        public string Set_Actualizar_imagenPersonal(int idPersonal, string nombreFileServer)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_PERSONAL_IMAGEN", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idPersonal", SqlDbType.Int).Value = idPersonal;
                        cmd.Parameters.Add("@nombreFoto", SqlDbType.VarChar).Value = nombreFileServer;

                        cmd.ExecuteNonQuery();
                        resultado = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

    }
}
