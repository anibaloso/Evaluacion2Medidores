using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DTO
{
    class Estacion
    {
        private int idEstacion;
        private String direccion;
        private int idRegion;
        private DateTime horario;
        private int maxPCarga;

        public int IdEstacion { get => idEstacion; set => idEstacion = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int IdRegion { get => idRegion; set => idRegion = value; }
        public DateTime Horario { get => horario; set => horario = value; }
        public int MaxPCarga { get => maxPCarga; set => maxPCarga = value; }
    }
}
