using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketsUtils
{
    public class ClienteSocket
    {
        private Socket comCliente;//esto representa la comunicacion que tenemos con el cliente
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteSocket(Socket comCliente)
        {
            this.comCliente = comCliente;
            Stream stream = new NetworkStream(this.comCliente);
            this.writer = new StreamWriter(stream);
            this.reader = new StreamReader(stream);
        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);//solo envio los datos
                this.writer.Flush();//con este flush se limpia y muestra que llegaron los datos
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CerrarConexion()
        {
            this.comCliente.Close();
        }


    }
}
