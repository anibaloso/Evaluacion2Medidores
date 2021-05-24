using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    class PuntoDeCarga
    {
        private int idPuntoDeCarga;
        private DateTime vidaUtil;
        private int capacidadMax;
        private string tipo;
        private int vehiculoDiario;
        private Boolean estadoSensores;
        private int precioCarga;

        public int IdPuntoDeCarga { get => idPuntoDeCarga; set => idPuntoDeCarga = value; }
        public DateTime VidaUtil { get => vidaUtil; set => vidaUtil = value; }
        public int CapacidadMax { get => capacidadMax; set => capacidadMax = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int VehiculoDiario { get => vehiculoDiario; set => vehiculoDiario = value; }
        public bool EstadoSensores { get => estadoSensores; set => estadoSensores = value; }
        public int PrecioCarga { get => precioCarga; set => precioCarga = value; }
    }
}
