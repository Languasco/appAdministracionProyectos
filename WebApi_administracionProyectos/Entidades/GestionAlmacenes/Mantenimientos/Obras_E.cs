using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.GestionAlmacenes.Mantenimientos
{
   public  class Obras_E
    {
        public int id_TD { get; set; }
        public string id_TipoTD { get; set; }
        public string descripcion_TipoObra { get; set; }
        public string Codigo_TD { get; set; }
        public string id_EstaCliente { get; set; }
        public string descripcion_TD { get; set; }
        public string id_Area { get; set; }

        public string nombre_area { get; set; }
        public string direccion_TD { get; set; }
        public string fechaRecepcion_TD { get; set; }
        public string fechaInicio_TD { get; set; }
        public string fechaTermino_TD { get; set; }
        public string id_Cliente_TD { get; set; }
        public string id_Colaborador_TD { get; set; }
        public string id_Ubigeo { get; set; }
        public string salidaMat_TD { get; set; }
        public string devolucionMat_TD { get; set; }
        public string transferenciaOrigen_TD { get; set; }
        public string transferenciaDestino_TD { get; set; }
        public string estado { get; set; }
        public string id_Empresa { get; set; }
        public string id_Delegacion { get; set; }
        public string id_Proyecto { get; set; }
    }
}
