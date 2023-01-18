﻿using Negocio.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Uploads
{
    public class Upload_BL
    {
        OleDbConnection cn;

        public int crear_archivoOrdenCompra( string idOrdenCompra, string nroOc, string tipoDoc, string opcionModal,string nombreArchivo, string nroDoc, string fecha, string importe)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ORDENCOMPRA_ADJUNTAR_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idOrdenCompra", SqlDbType.VarChar).Value = idOrdenCompra;
                        cmd.Parameters.Add("@nroOc", SqlDbType.VarChar).Value = nroOc;
                        cmd.Parameters.Add("@tipoDoc", SqlDbType.VarChar).Value = tipoDoc;
                        cmd.Parameters.Add("@opcionImportacion", SqlDbType.Int).Value = (opcionModal == "cotizacion") ? 1 : 2 ;
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;

                        cmd.Parameters.Add("@nroDoc", SqlDbType.VarChar).Value = nroDoc;
                        cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = fecha;
                        cmd.Parameters.Add("@importe", SqlDbType.VarChar).Value = importe;

                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        
        public int crear_archivoProveedor(int idProveedor, int tipDoc, string nombreArchivo, string idUsuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REGISTRO_PROVEEDOR_FILE_PROVEEDOR_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idProveedor", SqlDbType.Int).Value = idProveedor;
                        cmd.Parameters.Add("@tipDoc", SqlDbType.Int).Value = tipDoc; 
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public int crear_archivoDocumentosCab(int idDocumentoCab, string nombreArchivo, string idUsuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REGISTRO_FACTURAS_FILE_CAB_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idDocumentoCab", SqlDbType.Int).Value = idDocumentoCab; 
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        
        public int crear_archivosAdicionalesDocumentosCab(int idDocumentoCab, int tipoDoc, string nroDoc, string fechaDoc, string nombreArchivo , string idUsuario, string serieDoc)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_REGISTRO_FACTURAS_FILE_ADICIONALES_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idDocumentoCab", SqlDbType.Int).Value = idDocumentoCab;
                        cmd.Parameters.Add("@tipoDoc", SqlDbType.Int).Value = tipoDoc;
                        cmd.Parameters.Add("@nroDoc", SqlDbType.VarChar).Value = nroDoc;
                        cmd.Parameters.Add("@fechaDoc", SqlDbType.VarChar).Value = fechaDoc;

                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@serieDoc", SqlDbType.VarChar).Value = serieDoc;

                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        private OleDbConnection ConectarExcel(string rutaExcel)
        {
            cn = new OleDbConnection();
            try
            {
                cn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                cn.Open();
                return cn;
            }
            catch (Exception)
            {
                cn.Close();
                throw;
            }
        }

        public DataTable ListaExcel(string fileLocation)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT *FROM [Importar$]";

                OleDbDataAdapter da = new OleDbDataAdapter(sql, ConectarExcel(fileLocation));
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(dt);
                cn.Close();
            }
            catch (Exception)
            {
                cn.Close();
                throw;
            }
            return dt;
        }
        
        public string setAlmacenandoFile_Excel_cajaChica(string fileLocation, string nombreArchivo, int idLiquidacionCaja_Cab, string idUsuario)
        {
            string resultado = "";
            DataTable dt = new DataTable();

            try
            {
                dt = ListaExcel(fileLocation);

                using (SqlConnection con = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_DELETE_TEMPORAL_CAJA_CHICA", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.ExecuteNonQuery();
                    }

                    //guardando al informacion de la importacion
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {

                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_CAJA_CHICA";
                        bulkCopy.WriteToServer(dt);

                        //Actualizando campos 

                        string Sql = "UPDATE TEMPORAL_CAJA_CHICA SET nombreArchivo='" + nombreArchivo + "',  usuario_importacion='" + idUsuario + "', fechaBD=getdate() WHERE usuario_importacion IS NULL    ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }

                        //using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_GRABAR_EXCEL", con))
                        //{
                        //    cmd.CommandTimeout = 0;
                        //    cmd.CommandType = CommandType.StoredProcedure;
                        //    cmd.Parameters.Add("@idLiquidacionCaja_Cab", SqlDbType.Int).Value = idLiquidacionCaja_Cab;
                        //    cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = idUsuario;

                        //    cmd.ExecuteNonQuery();
                        //}
                    }
                    resultado = "OK";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
        
        public int crear_documentosCajaChica(int idLiquidacionCaja_Cab, string nombreArchivo, string idUsuario )
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_FILE_DOCUMENTOS_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idLiquidacionCaja_Cab", SqlDbType.Int).Value = idLiquidacionCaja_Cab;
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }
               
        public string setAlmacenandoFile_Excel_liquidacionesCab(string fileLocation, string nombreArchivo, int idLiquidacionCaja_Cab, string idUsuario)
        {
            string resultado = "";
            DataTable dt = new DataTable();

            try
            {
                dt = ListaExcel(fileLocation);

                using (SqlConnection con = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_DELETE_TEMPORAL_LIQUIDACIONES", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.ExecuteNonQuery();
                    }

                    //guardando al informacion de la importacion
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {

                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_LIQUIDACIONES";
                        bulkCopy.WriteToServer(dt);

                        //Actualizando campos 

                        string Sql = "UPDATE TEMPORAL_LIQUIDACIONES SET nombreArchivo='" + nombreArchivo + "',  usuario_importacion='" + idUsuario + "', fechaBD=getdate() WHERE usuario_importacion IS NULL    ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_GRABAR_EXCEL_CTA_CONTABLE", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@idLiquidacionCaja_Cab", SqlDbType.Int).Value = idLiquidacionCaja_Cab;
                            cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = idUsuario;

                            cmd.ExecuteNonQuery();
                        }
                    }
                    resultado = "OK";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public int crear_documentosCajaChica_det(int idLiquidacionCaja_det, string nombreArchivo, string idUsuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_FILE_DOCUMENTOS_DET_GRABAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idLiquidacionCaja_det", SqlDbType.Int).Value = idLiquidacionCaja_det;
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }


        public DataTable get_datosCargados_excelCajaChica(string id_usuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_CAJA_CHICA_LISTADO_EXCEL_ERRORES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = id_usuario;
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


        public int crear_documentosPendienteArchivo(int idDocumento, string nombreArchivo, string idUsuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_ADJUNTAR_DOCUMENTOS_SAVE_FILE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idDocumento", SqlDbType.Int).Value = idDocumento;
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }



        public string setAlmacenandoFile_Excel_distribucion(string fileLocation, string nombreArchivo, int idFacturaCab, string idUsuario)
        {
            string resultado = "";
            DataTable dt = new DataTable();

            try
            {
                dt = ListaExcel(fileLocation);

                using (SqlConnection con = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_DELETE_TEMPORAL_DISTRIBUCION", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = idUsuario;
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {

                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_DISTRIBUCION";
                        bulkCopy.WriteToServer(dt);

                        //Actualizando campos 

                        string Sql = "UPDATE TEMPORAL_DISTRIBUCION SET nombreArchivo='" + nombreArchivo + "',  usuario_importacion='" + idUsuario + "', fechaBD=getdate() WHERE usuario_importacion IS NULL    ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    resultado = "OK";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }

        public DataTable get_datosCargados_excelDistribucion(string id_usuario)
        {
            DataTable dt_detalle = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_APROBAR_FACTURAS_LISTADO_EXCEL_ERRORES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.VarChar).Value = id_usuario;
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


        public int crear_archivoProductos(string idProducto, string nombreArchivo, string idUsuario)
        {
            int resultado = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(bdConexion.cadenaBDcx()))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_PROY_W_MANT_PRODUCTO_SAVE_FILE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idProducto", SqlDbType.VarChar).Value = idProducto;
                        cmd.Parameters.Add("@nombreArchivo", SqlDbType.VarChar).Value = nombreArchivo;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.VarChar).Value = idUsuario;

                        cmd.Parameters.Add("@name_bd", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        resultado = Convert.ToInt32(cmd.Parameters["@name_bd"].Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return resultado;
        }




    }
}
