using System.Collections.Generic;
using System.IO;
using ServicioComunicacionModel.DTO;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorConsumoDALArchivos : IMedidorConsumoDAL
    {
        //creo el archivo        necesito importarlo|v y el getcurrent me da mi ubicacion

        private string archivo = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "Consumo.txt";

        //creo una lista estatica para que pueda ser buscada aqui los medidores de consumo

        public static List<int> listaMConsumo = new List<int>()
            {
                1,2,3,4,5,6
            };


        //patron singleton
        //1.constructor privado
        private MedidorConsumoDALArchivos()
        {

        }

        //2.Atributo tipo estatico privadode la misma instancia
        private static IMedidorConsumoDAL instancia;

        //3.Un metodo estatico que permita obtener la unica instancia
        public static IMedidorConsumoDAL GetInstance()
        {
            if (instancia == null)
            {
                instancia = new MedidorConsumoDALArchivos();
            }
            return instancia;
        }

        public List<MedidorConsumo> GetAll()
        {
            List<MedidorConsumo> lista = new List<MedidorConsumo>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string linea = null;
                    do
                    {
                        linea = reader.ReadLine();
                        if (linea != null)
                        {
                            string[] textoArray = linea.Split('|');
                            MedidorConsumo m = new MedidorConsumo()
                            {
                                Fecha = textoArray[0],
                                IdMedidor = textoArray[1],
                                CantidadConsumo = textoArray[2],
                                Estado = textoArray[3]
                            };
                            lista.Add(m);
                        }

                    } while (linea != null);
                }
            }
            catch (IOException ex)
            {
                lista = null;
            }
            return lista;
        }

        public void Save(MedidorConsumo m)
        {
            try
            {
                //el using lo que hace me ahorra crear la instancia y ademas hace el close().
                //con esto lo que hace es que cuando se crea el archivo y no existecrea uno nuevo
                //pero si ya existe uno lo que hara es escribir aparte en el mismo archivo
                //si el boleano fuera false se sobreescribiria siempre el archivo

                using (StreamWriter writer = new StreamWriter(archivo, true))

                {
                    writer.WriteLine(m);
                    writer.Flush();
                }
            }
            catch (IOException ex)
            {

            }

        }

        public List<int> ObtenerMedidoresConsumo => ObtenerMedidoresConsumo;
    }

}
