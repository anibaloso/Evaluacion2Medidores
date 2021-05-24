using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioComunicacionModel.DAL
{
    public interface IMedidorTraficoDAL
    {
        void Save(MedidorTrafico m);

        List<MedidorTrafico> GetAll();

        List<int> ObtenerMedidoresTrafico { get; }
    }
}
