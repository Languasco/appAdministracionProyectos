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
    
    public partial class tbl_Areas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Areas()
        {
            this.tbl_CuentaCorriente_Servicio = new HashSet<tbl_CuentaCorriente_Servicio>();
            this.tbl_Obra_TD = new HashSet<tbl_Obra_TD>();
            this.tbl_Usuarios = new HashSet<tbl_Usuarios>();
            this.tbl_Cuadrilla_Area = new HashSet<tbl_Cuadrilla_Area>();
        }
    
        public int id_Area { get; set; }
        public string nombre_area { get; set; }
        public string descripcion_area { get; set; }
        public Nullable<int> CodigoEdelnor { get; set; }
        public string proceso_area { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string tipoServicio { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_CuentaCorriente_Servicio> tbl_CuentaCorriente_Servicio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Obra_TD> tbl_Obra_TD { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Usuarios> tbl_Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Area> tbl_Cuadrilla_Area { get; set; }
    }
}
