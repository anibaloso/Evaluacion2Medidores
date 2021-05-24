using ServicioComunicacionModel.DAL;
using ServicioComunicacionModel.DTO;
using SocketsUtils;
using System;
using System.Globalization;
using System.Linq;

namespace ServicioComunicacion.Hilos
{
    class HiloCliente
    {
        private ClienteSocket clienteSocket;
        private IMedidorConsumoDAL dal = MedidorConsumoFactory.CreateDAL();
        private IMedidorTraficoDAL dalt = MedidorTraficoDALFactory.CreateDAL();
        public HiloCliente(ClienteSocket clienteSocket)
        {
            this.clienteSocket = clienteSocket;
        }

        public void Ejecutar()
        {
            string linea = null, linea2 = null;
            DateTime date;
            string fecha;
            int nroMedidor;
            string tipo;
            DateTime horaFecha = DateTime.Now;
            string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
            //en una variable DateTime le doy la fecha ahora
            CultureInfo provider = CultureInfo.InvariantCulture;
            Console.WriteLine("Ingrese fecha | Nro medidor | tipo de medidor");
            clienteSocket.Escribir("Ingrese fecha | Nro medidor | tipo de medidor");
            linea = clienteSocket.Leer().Trim();
            //se captura lo que ingresan
            string[] arreglo = linea.Trim().Split('|');                                         //se crea un arreglo y se separa cada palabra y se guarda en cada posicion
            string[] formatos = { "yyyyMMddHHmmss" };                                           //se crea un formato para guardar el tipo date
            Console.WriteLine(linea);
            try
            {
                //se le da un valor a cada posicion del arreglo fecha-nroMedidor-TipoMedidor
                fecha = arreglo[0].Trim().Replace("-", "");
                nroMedidor = Int32.Parse(arreglo[1].Trim().Replace("-", ""));
                tipo = arreglo[2].Trim().Replace(" ", "");

                //se intenta darle formato ala fecha 
                if (DateTime.TryParseExact(fecha, formatos, System.Globalization.CultureInfo.InvariantCulture,
                                                                    System.Globalization.DateTimeStyles.None, out date))
                {

                }
                else                              //si no se puede dar ese formato salta error 
                {

                    //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(horaFechaString + " | " + arreglo[1] + " | ERROR");
                    Console.ResetColor();
                    clienteSocket.CerrarConexion();

                }

                //preguntamos si es consumo o trafico 
                if (tipo == "consumo")
                {
                    //se comprueba la diferencia entre la fecha ingresada y la hora del servidor 
                    var minutos = (horaFecha - date).TotalMinutes;                                      //se ve la diferencia entre las fechas en minutos
                    //mayor que 30min se rechaza la entrada 
                    if (minutos > 30)
                    {

                        //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                        Console.ForegroundColor = ConsoleColor.Red;                                         //cambio color letra
                        Console.WriteLine(horaFechaString + " | " + arreglo[1] + " | ERROR");               //mensaje de error
                        Console.ResetColor();
                        clienteSocket.CerrarConexion();

                    }
                    //si esta entre los ultimos 30minutos y ademas se encuentra el numero del medidor se procede
                    else if (minutos <= 30 && minutos > 0 && ComprobarMedidorConsumo(nroMedidor))
                    {
                        clienteSocket.Escribir(horaFecha + "| WAIT");
                        Console.WriteLine(horaFecha + "| WAIT");
                        clienteSocket.Escribir("nroSerie | fecha | tipo | valor | {estado} | UPDATE");
                        Console.WriteLine("nroSerie | fecha | tipo | valor | {estado} | UPDATE");
                        //TODO ACTUALIZAR EL UPDATE

                        linea2 = clienteSocket.Leer().Trim();                                           //se lee y guarda los nuevos datos ingresados
                        Console.WriteLine(linea2);

                        string[] arreglo2 = linea2.Split('|');
                        string nserie = arreglo2[0].Trim(), nfecha = arreglo2[1].Trim(), ntipo = arreglo2[2].Trim(), nvalor = arreglo2[3].Trim().ToString(), nupdate = arreglo2[5].Trim().ToUpper();
                        int dato = Int32.Parse(nvalor);
                        Console.WriteLine(dato);

                        if (dato > 0 && dato <= 1000)
                        {
                            //Console.WriteLine(nserie);
                            //Console.WriteLine(nroMedidor);

                            if (Int32.Parse(nserie).Equals(nroMedidor))
                            {

                                //Console.WriteLine("si es el mismo numero de medidor de consumo");
                                int nestado = Convert.ToInt32(arreglo2[4]);

                                DateTime horaDate;

                                if (DateTime.TryParseExact(fecha, formatos, System.Globalization.CultureInfo.InvariantCulture,
                                                                        System.Globalization.DateTimeStyles.None, out horaDate))
                                {

                                }
                                string horaString = horaDate.ToString();
                                //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                                //Console.WriteLine(horaFechaString);

                                if (nupdate.Equals("UPDATE")) { 
                                MedidorConsumo m = new MedidorConsumo();

                                switch (nestado)
                                {
                                    case -1:
                                        m.Estado = "Error de lectura";
                                        break;
                                    case 0:
                                        m.Estado = "OK";
                                        break;
                                    case 1:
                                        m.Estado = "Punto de carga lleno";
                                        break;
                                    case 2:
                                        m.Estado = "Requiere mantención preventiva";
                                        break;
                                    default:
                                        m.Estado = ("sin estado");
                                        break;
                                }
                                //guardamos los datos en el archivo
                                m.Fecha = horaString;
                                m.CantidadConsumo = nvalor;
                                m.IdMedidor = nserie;
                                dal.Save(m);
                                clienteSocket.CerrarConexion();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                                    Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                                    Console.ResetColor();
                                    clienteSocket.CerrarConexion();
                                }
                            }
                            else
                            {
                                //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                                Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                                Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                                Console.ResetColor();
                                clienteSocket.CerrarConexion();
                            }
                        }
                        else
                        {
                            //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                            Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                            Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                            Console.ResetColor();
                            clienteSocket.CerrarConexion();
                        }
                    }
                    else
                    {
                        //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                        Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                        Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                        Console.ResetColor();
                        clienteSocket.CerrarConexion();
                    }
                }
                else if (tipo == "trafico")
                {
                    //Console.WriteLine("paso el trafico");
                    var minutos = (horaFecha - date).TotalMinutes;
                    if (minutos > 30)
                    {

                        //string horaFechaString = horaFecha.ToString("yyyy-MM-dd-HH-mm-ss");
                        Console.ForegroundColor = ConsoleColor.Red;                                         //cambio color letra
                        Console.WriteLine(horaFechaString + " | " + arreglo[1] + " | ERROR");               //mensaje de error
                        Console.ResetColor();
                        clienteSocket.CerrarConexion();

                    }
                    //si esta entre los ultimos 30minutos y ademas se encuentra el numero del medidor se procede
                    else if (minutos <= 30 && minutos > 0 && ComprobarMedidorConsumo(nroMedidor))
                    {
                        clienteSocket.Escribir(horaFecha + "| WAIT");
                        Console.WriteLine(horaFecha + "| WAIT");
                        clienteSocket.Escribir("nroSerie | fecha | tipo | valor | {estado} | UPDATE");
                        Console.WriteLine("nroSerie | fecha | tipo | valor | {estado} | UPDATE");

                        //clienteSocket.Escribir("Aqui cago la wea");
                        //Console.WriteLine("cago aqui");

                        linea2 = clienteSocket.Leer().Trim();                                           //se lee y guarda los nuevos datos ingresados
                        Console.WriteLine(linea2);


                        string[] arreglo2 = linea2.Split('|');
                        string nserie = arreglo2[0].Trim(), nfecha = arreglo2[1].Trim(), ntipo = arreglo2[2].Trim(), nvalor = arreglo2[3].Trim().ToString(),nupdate=arreglo2[5].ToUpper().Trim();

                        if (Int32.Parse(nvalor) > 0 && Int32.Parse(nvalor) <= 1000)
                        {
                            //Console.WriteLine(nserie);
                            //Console.WriteLine(nroMedidor);

                            if (Int32.Parse(nserie).Equals(nroMedidor))
                            {

                                //Console.WriteLine("si es el mismo numero de medidor de consumo");
                                int nestado = Convert.ToInt32(arreglo2[4]);

                                string horaString = horaFecha.ToString();

                                if (nupdate.Equals("UPDATE"))
                                {
                                    MedidorTrafico t = new MedidorTrafico();

                                    switch (nestado)
                                    {
                                        case -1:
                                            t.Estado = "Error de lectura";
                                            break;
                                        case 0:
                                            t.Estado = "OK";
                                            break;
                                        case 1:
                                            t.Estado = "Punto de carga lleno";
                                            break;
                                        case 2:
                                            t.Estado = "Requiere mantención preventiva";
                                            break;
                                        default:
                                            t.Estado = ("sin estado");
                                            break;
                                    }
                                    //guardamos los datos en el archivo
                                    t.Fecha = horaString;
                                    t.CantidadConsumo = nvalor;
                                    t.IdMedidor = nserie;
                                    dalt.Save(t);
                                    clienteSocket.CerrarConexion();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                                    Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                                    Console.ResetColor();
                                    clienteSocket.CerrarConexion();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                                Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                                Console.ResetColor();
                                clienteSocket.CerrarConexion();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                            Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                            Console.ResetColor();
                            clienteSocket.CerrarConexion();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;                             //cambio color letra
                        Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");         //mensaje de error
                        Console.ResetColor();
                        clienteSocket.CerrarConexion();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;                                         //cambio color letra
                    Console.WriteLine(horaFechaString + " | " + arreglo[1] + " | ERROR");               //mensaje de error
                    Console.ResetColor();
                    clienteSocket.CerrarConexion();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;                                     //cambio color letra
                Console.WriteLine(horaFecha + " | " + arreglo[1] + " | ERROR");                 //mensaje de error
                Console.ResetColor();
                clienteSocket.CerrarConexion();

            }

        }

        //se recorre la lista en busca de si se encuentra el numero del medidor
        private Boolean ComprobarMedidorConsumo(int i)
        {
            var lista = MedidorConsumoDALArchivos.listaMConsumo.ToList();

            if (lista.Contains(i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //se recorre la lista en busca de si se encuentra el numero del medidor
        private Boolean ComprobarMedidorTrafico(int i)
        {
            var lista = MedidorTraficoDALArchivos.listaMTrafico.ToList();
            if (lista.Contains(i))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
