using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    class ZonaHoraria
    {
        private int idZonaHoraria;
        private string zHoraria;
        private float tarifa;

        public int IdZonaHoraria { get => idZonaHoraria; set => idZonaHoraria = value; }
        public string ZHoraria { get => zHoraria; set => zHoraria = value; }
        public float Tarifa { get => tarifa; set => tarifa = value; }
    }
}
