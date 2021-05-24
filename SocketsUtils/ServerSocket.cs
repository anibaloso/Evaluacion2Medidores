using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketsUtils

{
    public class ServerSocket //lo cambie a publico **1**
    {
        private int puerto;
        private Socket servidor;
        

        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        public bool Iniciar() //creamos todas estas firmas 
        {
            try
            {
                //1.crear el socket
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //creee una nueva instancia de socket

                //2.tomar control del puerto
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));

                //3.definir cuantos clientes puede atender "simultaneamente"
                this.servidor.Listen(10);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        
        //TODO: Cambiar obtener, escribir y leer  a un hilo independiente   
        public ClienteSocket ObtenerCliente()
        {
            try
            {
                return new ClienteSocket(this.servidor.Accept()); //es la sesion que tengo con el cliente el objeto de tipo socket
                //al pasar a esta linea quiere decir que un cliente se conecto
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
    }
}