using ClienteSocketApp.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string ip = ConfigurationManager.AppSettings["ip"];
                int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Conectando con el servidor {0} en el puerto {1}", ip, puerto);
                ClienteSocket clienteSocket = new ClienteSocket(puerto, ip);
                //Console.WriteLine("no a conectado aun");

                if (clienteSocket.Conectar())
                {
                    string mensaje = null;

                    try
                    {

                        Console.WriteLine("Ingrese fecha | Nro medidor | tipo de medidor");
                        mensaje = Console.ReadLine().Trim();
                        //Console.WriteLine(mensaje);
                        clienteSocket.Escribir(mensaje);
                        Console.WriteLine("nroSerie | fecha | tipo | valor | {estado} | UPDATE");
                        mensaje = Console.ReadLine().Trim();
                        clienteSocket.Escribir(mensaje);
                        Console.ReadKey();

                    }
                    catch (Exception ex)
                    {
                        clienteSocket.Desconectar();
                    }

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error al conectar con el servidor {0} en el puerto {1}", ip, puerto);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
