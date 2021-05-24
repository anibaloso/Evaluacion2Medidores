using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public interface IMedidorConsumoDAL
    {
        void Save(MedidorConsumo m);

        List<MedidorConsumo> GetAll();

        List<int> ObtenerMedidoresConsumo { get; }
    }
}
