using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.GestionAlmacenes.Mantenimientos
{
    public class Almacenes_E
    {
        public int id_Almacen { get; set; }
        public string id_Empresa { get; set; }
        public string ruc_empresa { get; set; }

        public string id_Local { get; set; }
        public string nombre_local { get; set; }

        public string id_Delegacion { get; set; }
        public string codigo_delegacion { get; set; }
        public string nombre_delegacion { get; set; }

        public string id_TipoAlmacen { get; set; }
        public string nombre_TipoAlmacen { get; set; }

        public string descripcion_Almacen { get; set; }
        public string direccion_Almacen { get; set; }
        public string MatNormall_Almacen { get; set; }
        public string MatUsado_Almacen { get; set; }
        public string MatBaja_Almacen { get; set; }
        public string Stock_EmpresObra { get; set; }
        public string Stock_EmpresPersonal { get; set; }
        public string estado { get; set; }

        public string id_Proyecto { get; set; }
        public string nombre_proyecto { get; set; }
    }
}
