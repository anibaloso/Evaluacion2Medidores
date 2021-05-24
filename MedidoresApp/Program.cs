using ServicioComunicacion.Hilos;
using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicacion
{
    public partial class Programs
    {
        static IMedidorConsumoDAL dal = MedidorConsumoFactory.CreateDAL();
        static IMedidorTraficoDAL dalt = MedidorTraficoDALFactory.CreateDAL();

        static bool Menu()
        {
            return true;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando hilo del Server");
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            HiloServer hiloServer = new HiloServer(puerto);
            Thread t = new Thread(new ThreadStart(hiloServer.Ejecutar));

            t.Start();

        }
    }
}