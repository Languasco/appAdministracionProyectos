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
    
    public partial class tbl_Web_Page
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Web_Page()
        {
            this.tbl_Web_Aceesos = new HashSet<tbl_Web_Aceesos>();
        }
    
        public int id_WebPage { get; set; }
        public int id_Opcion { get; set; }
        public string nombre_webpage { get; set; }
        public string url_webpage { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
    
        public virtual tbl_Definicion_Opciones tbl_Definicion_Opciones { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Web_Aceesos> tbl_Web_Aceesos { get; set; }
    }
}
