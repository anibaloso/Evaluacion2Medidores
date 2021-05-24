using System.Collections.Generic;
using System.IO;
using ServicioComunicacionModel.DTO;

namespace ServicioComunicacionModel.DAL
{
    public class MedidorTraficoDALArchivos : IMedidorTraficoDAL
    {

        private string archivo = Directory.GetCurrentDirectory()
            + Path.DirectorySeparatorChar + "Trafico.txt";
        
        //lista estatica de medidores de trafico 
        public static List<int> listaMTrafico = new List<int>()
        {
            3,5,8,9,10,11
        };

        //singleton paso 1
        private MedidorTraficoDALArchivos()
        {

        }
        //atributo estatico privado de la misma instancia
        private static IMedidorTraficoDAL instancia;

        //metodo estatico que permite obtener la unica instancia
        public static IMedidorTraficoDAL GetInstance()
        {
            if (instancia == null)
            {
                instancia = new MedidorTraficoDALArchivos();
            }
            return instancia;
        }


        public List<MedidorTrafico> GetAll()
        {
            List<MedidorTrafico> lista = new List<MedidorTrafico>();
            try
            {
                using (StreamReader reader= new StreamReader(archivo))
                {
                    string linea = null;
                    do
                    {
                        linea = reader.ReadLine();
                        if (linea != null)
                        {
                            string[] textoArray = linea.Split('|');
                            MedidorTrafico m = new MedidorTrafico()
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
            }catch(IOException ex)
            {
                lista = null;
            }
            return lista;
        }

        public void Save(MedidorTrafico m)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(m);
                    writer.Flush();
                }
            }catch(IOException ex)
            {

            }
        }


        public List<int> ObtenerMedidoresTrafico => ObtenerMedidoresTrafico;
    }
}
