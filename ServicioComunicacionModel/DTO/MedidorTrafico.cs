using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    public class MedidorTrafico
    {
        private String idMedidor = null;
        private String cantidadConsumo = null;
        private String fecha = null;
        private String estado = null;

        public string IdMedidor { get => idMedidor; set => idMedidor = value; }
        public string CantidadConsumo { get => cantidadConsumo; set => cantidadConsumo = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Estado { get => estado; set => estado = value; }



        public override string ToString()
        {
            return "{\n" +
                "   \"Fecha\": \"" + fecha + "\" \n" +
                "   \"IdMedidor\": \"" + idMedidor + "\" \n" +
                "   \"cantidadConsumo\": \"" + cantidadConsumo + "\" \n" +
                "   \"estado\": \"" + estado + "\"\n" +
                "   UPDATE \n" +
                "}";
        }
    }
}
