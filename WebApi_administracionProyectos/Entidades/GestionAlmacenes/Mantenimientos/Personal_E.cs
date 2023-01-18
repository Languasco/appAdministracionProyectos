using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.GestionAlmacenes.Mantenimientos
{
    public class Personal_E
    {
		public int id_Personal { get; set; }
		public string nroDoc_Personal { get; set; }
		public string apellidos_Personal { get; set; }
		public string nombres_Personal { get; set; }
		public string telefono_Personal { get; set; }
		public string direccion_Personal { get; set; }
		public string fechaIngreso_Personal { get; set; }
		public string fechaCese_Personal { get; set; }
		public string estado { get; set; }

	}
}
