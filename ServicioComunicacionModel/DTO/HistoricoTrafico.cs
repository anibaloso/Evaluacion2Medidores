using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    class HistoricoTrafico
    {
        private int idHistoricoTrafico;
        private int idMedidorDeTrafico;
        private DateTime fecha;
        private int traficoTemp;

        public int IdHistoricoTrafico { get => idHistoricoTrafico; set => idHistoricoTrafico = value; }
        public int IdMedidorDeTrafico { get => idMedidorDeTrafico; set => idMedidorDeTrafico = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public int TraficoTemp { get => traficoTemp; set => traficoTemp = value; }
    }
}
