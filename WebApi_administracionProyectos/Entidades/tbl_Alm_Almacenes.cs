//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Alm_Almacenes
    {
        public int id_Almacen { get; set; }
        public int id_Empresa { get; set; }
        public int id_Local { get; set; }
        public int id_Delegacion { get; set; }
        public int id_TipoAlmacen { get; set; }
        public string descripcion_Almacen { get; set; }
        public string direccion_Almacen { get; set; }
        public string MatNormall_Almacen { get; set; }
        public string MatUsado_Almacen { get; set; }
        public string MatBaja_Almacen { get; set; }
        public string stock_DetalleGuia { get; set; }
        public string Stock_multiOtContable { get; set; }
        public string stock_OtGeneral { get; set; }
        public string Stock_EmpresObra { get; set; }
        public string Stock_EmpresPersonal { get; set; }
        public string Anterior_Almacen { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string MatCustodia_almacen { get; set; }
        public Nullable<int> id_Proyecto { get; set; }
    
        public virtual tbl_Alm_Tipos_Almacen tbl_Alm_Tipos_Almacen { get; set; }
        public virtual tbl_Locales tbl_Locales { get; set; }
    }
}
