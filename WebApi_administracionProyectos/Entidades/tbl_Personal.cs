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
    
    public partial class tbl_Personal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Personal()
        {
            this.tbl_Cuadrilla_Personal = new HashSet<tbl_Cuadrilla_Personal>();
        }
    
        public int id_Personal { get; set; }
        public string nroDoc_Personal { get; set; }
        public string tipoDoc_Personal { get; set; }
        public string apellidos_Personal { get; set; }
        public string nombres_Personal { get; set; }
        public string direccion_Personal { get; set; }
        public string telefono_Personal { get; set; }
        public Nullable<decimal> costoMo_Personal { get; set; }
        public Nullable<System.DateTime> fechaIngreso_Personal { get; set; }
        public Nullable<int> id_Cargo { get; set; }
        public string tipoPersonal { get; set; }
        public Nullable<System.DateTime> fechaCese_Personal { get; set; }
        public string retiraMate_Personal { get; set; }
        public string retiraEquipamiento_Personal { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string fotoBase64 { get; set; }
        public Nullable<int> id_Empresa { get; set; }
        public Nullable<int> id_Delegacion { get; set; }
        public Nullable<int> id_Proyecto { get; set; }
    
        public virtual tbl_Cargo_Personal tbl_Cargo_Personal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cuadrilla_Personal> tbl_Cuadrilla_Personal { get; set; }
    }
}