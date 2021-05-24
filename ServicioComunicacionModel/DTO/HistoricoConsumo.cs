using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    class HistoricoConsumo
    {
        private int idHistoricoConsumo;
        private int idMedidorConsumo;
        private DateTime fecha;
        private float cantidadConsumo;

        public int IdHistoricoConsumo { get => idHistoricoConsumo; set => idHistoricoConsumo = value; }
        public int IdMedidorConsumo { get => idMedidorConsumo; set => idMedidorConsumo = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public float CantidadConsumo { get => cantidadConsumo; set => cantidadConsumo = value; }
    }
}
