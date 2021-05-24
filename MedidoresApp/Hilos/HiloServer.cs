
using SocketsUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioComunicacion.Hilos
{
    public class HiloServer
    {
        private ServerSocket server;
        private int puerto;
        public HiloServer(int puerto)
        {
            this.puerto = puerto;
        }

        public void Ejecutar()
        {
            //Console.WriteLine("Iniciando servidor en puerto {0}", puerto);
            this.server = new SocketsUtils.ServerSocket(puerto);
            if (this.server.Iniciar())
            {
                //Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    //Console.WriteLine("Esperando Clientes...");
                    ClienteSocket cliente = this.server.ObtenerCliente();
                    //Crear una instancia del hilo del Cliente
                    HiloCliente hiloCliente = new HiloCliente(cliente);
                    Thread t = new Thread(new ThreadStart(hiloCliente.Ejecutar));
                    t.IsBackground = false;
                    t.Start();
                }
            }
        }

    }
}
