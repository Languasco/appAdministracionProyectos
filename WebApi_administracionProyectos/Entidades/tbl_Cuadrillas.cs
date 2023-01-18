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
    
    public partial class tbl_Cuadrillas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Cuadrillas()
        {
            this.tbl_Cuadrilla_Area = new HashSet<tbl_Cuadrilla_Area>();
            this.tbl_Cuadrilla_Colaboradora = new HashSet<tbl_Cuadrilla_Colaboradora>();
            this.tbl_Cuadrilla_Delegacion = new HashSet<tbl_Cuadrilla_Delegacion>();
            this.tbl_Cuadrilla_Personal = new HashSet<tbl_Cuadrilla_Personal>();
        }
    
        public int id_Cuadrilla { get; set; }
        public Nullable<int> id_Empresa { get; set; }
        public string codigo_Cuadrilla { get; set; }
        public string descripcion_Cuadrilla { get; set; }
        public string tipo_Cuadrilla { get; set; }
        public Nullable<int> id_colaborador { get; set; }
        public Nullable<int> id_Local { get; set; }
        public string salidaMat_Cuadrilla { get; set; }
        public string instalacionMat_Cuadrilla { get; set; }
        public string Anterior_Codigo { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public Nullable<int> id_Delegacion { get; set; }
        public Nullable<int> id_Proyecto { get; set; }
        public Nullable<int> id_vehiculo { get; set; }
        public string nro_placa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Area> tbl_Cuadrilla_Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Colaboradora> tbl_Cuadrilla_Colaboradora { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Delegacion> tbl_Cuadrilla_Delegacion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Personal> tbl_Cuadrilla_Personal { get; set; }
    }
}
