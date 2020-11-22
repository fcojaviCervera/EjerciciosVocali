using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Ejercicio1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            /***    
             * 
             *  Dado un conjunto de N periodos, calcular la suma de estos teniendo en cuenta lo siguiente:
                 Un periodo está identificado por un id único, y tiene una hora de Inicio (mayor o
                igual que 00:00) y una hora de fin (menor o igual que 23:59).

                Asumiremos que la hora de Inicio siempre será menor que la hora de fin.

                La suma de 2 periodos se calcula de la siguiente forma: los periodos solapados
                se combinan en uno nuevo, de forma que el resultado final sea otro conjunto de
                periodos que no se solapen.

                El conjunto de períodos se especificará mediante un fichero CSV donde cada
                período vendrá representado por la terna id,Ini,fin con formato:

                o Id: string. Ej: A
                o Ini: string donde se indica una hora en formato mm:ss. Ej: 08:15
                o Fin: string donde se indica una hora en formato mm:ss. Ej: 08:15

                En el ejemplo siguiente la suma de Periodo 1 y Periodo 2 da como resultado tres
                periodos:
                - [id=A, Ini=03:00, fin=05:59]
                - [id=AB, Ini=06:00, fin=08:00]
                - [id=B, Ini=08:01, fin=09:00]
             * 
             */

            //Consideraciones a tener en cuenta
            /**
             * Tener en cuenta valores nulos o vacios, si hay valor vacio se descarta el conjunto
             * MInimo tiene que haber un conjunto, y el resultado del programa será el mismo conjunto
             * Tener en cuenta repetidos, si hay un repetido se elimina el resultado
             * Si ya existe un periodo con el mismo id, se descarta              
             * Si no se indica alguna de las horas, o no cumplen el formato mm:ss se descarta 
             
             
             */
            Console.WriteLine("Ejercicio 1");

            string fichero = ConfigurationManager.AppSettings["RutaEjecucion"] + ConfigurationManager.AppSettings["NombreFichero"];

            Console.WriteLine("Por favor, asegurese de que el fichero CSV se encuentra en la carpeta: " + fichero);
            Console.ReadKey();

            HashSet<Periodo> periodos = LeerFichero(fichero);

            HashSet<Periodo> periodosProcesados = ProcesarEntrada(periodos);

            periodos.UnionWith(periodosProcesados);

            
            Console.WriteLine("Periodos tras procesar el fichero");

            foreach (var item in periodos)
            {
                Console.WriteLine("\tPeriodo -> Id : {0} | Ini: {1} | Fin: {2}", item.Id, item.Ini, item.Fin);
            }


            Console.WriteLine("Fin de procesamiento de fichero");
        }

        private static HashSet<Periodo> ProcesarEntrada(HashSet<Periodo> periodos)
        {
            HashSet<Periodo> salida = new HashSet<Periodo>();
            List<Periodo> copia = new List<Periodo>();
            copia = periodos.ToList();


            foreach (var item in periodos)
            {
                //Se elimina el propio elemento para evitar iterar sobre el mismo                
                //copia.Remove(item);

                foreach (var item2 in copia)
                {
                    Periodo interseccion = item.Intersect(item2);
                    if (interseccion != null)
                    {
                        salida.Add(interseccion);
                    }
                }

            }
            return salida;

        }

        public static HashSet<Periodo> LeerFichero(string rutaCompleta)
        {
            HashSet<Periodo> periodos = new HashSet<Periodo>();
            try
            {
                var reader = new StreamReader(File.OpenRead(rutaCompleta));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    Periodo periodo = new Periodo(values[0].Trim(), values[1].Trim(), values[2].Trim());

                    if (periodo.IsValid())
                    {
                        if (periodos.Add(periodo))
                        {
                            Console.WriteLine("\tPeriodo añadido al conjunto-> Id : {0} | Ini: {1} | Fin: {2}", periodo.Id, periodo.Ini, periodo.Fin);
                        }
                        else
                        {
                            Console.WriteLine("\tPeriodo ya existente en el conjunto-> Id : {0} | Ini: {1} | Fin: {2}", periodo.Id, periodo.Ini, periodo.Fin);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tPeriodo incorrecto, no se añade al conjunto-> Id : {0} | Ini: {1} | Fin: {2}", periodo.Id, periodo.Ini, periodo.Fin);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No se ha encontrado el fichero especificado, por favor, revise si el fichero existe y la ruta está bien indicado en el fichero de configuracion");
            }
            return periodos;
        }
    }
}
