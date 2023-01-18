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
    
    public partial class tbl_Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Usuarios()
        {
            this.tbl_Web_Aceesos = new HashSet<tbl_Web_Aceesos>();
            this.tbl_Empresa_Usuario = new HashSet<tbl_Empresa_Usuario>();
        }
    
        public int id_Usuario { get; set; }
        public string nrodoc_usuario { get; set; }
        public string apellidos_usuario { get; set; }
        public string nombres_usuario { get; set; }
        public string email_usuario { get; set; }
        public string Adm_Usuario { get; set; }
        public string Sys_Usuario { get; set; }
        public int id_Cargo { get; set; }
        public int id_Area { get; set; }
        public string tipo_usuario { get; set; }
        public Nullable<int> id_Empresa_Pertenece { get; set; }
        public string fotoBase64 { get; set; }
        public string fotourl { get; set; }
        public string login_usuario { get; set; }
        public string contrasenia_usuario { get; set; }
        public Nullable<int> id_Perfil { get; set; }
        public int estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string Acceso_Movil_Tipo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Web_Aceesos> tbl_Web_Aceesos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Empresa_Usuario> tbl_Empresa_Usuario { get; set; }
        public virtual tbl_Areas tbl_Areas { get; set; }
        public virtual tbl_Cargo_Personal tbl_Cargo_Personal { get; set; }
    }
}