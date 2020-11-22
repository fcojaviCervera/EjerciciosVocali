using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Ejercicio1
{
    class Program
    {

        static void Main(string[] args)
        {
            //Consideraciones a tener en cuenta
            /**
             * Tener en cuenta valores nulos o vacios, si hay valor vacio se descarta el conjunto
             * Minimo tiene que haber un conjunto, y el resultado del programa será el mismo conjunto
             * Tiene en cuenta repetidos, si hay un repetido se elimina el resultado
             * Si ya existe un periodo con el mismo id, se descarta              
             * Si no se indica alguna de las horas, o no cumplen el formato mm:ss se descarta              
             * Se puede ejecutar indicando la ruta en el fichero de configuracion o indicandola por la linea de comandos.
             */
            Console.WriteLine("Ejercicio 1");

            string fichero;
            if (args.Length != 0)
            {
                fichero = args[0];
                Console.WriteLine("Se va a procesar el fichero: " + fichero);
            }
            else
            {
                fichero = ConfigurationManager.AppSettings["RutaEjecucion"] + ConfigurationManager.AppSettings["NombreFichero"];
                Console.WriteLine("Por favor, asegurese de que el fichero CSV se encuentra en la carpeta: " + fichero);

            }
            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
            HashSet<Periodo> periodos = LeerFichero(fichero);
            if (periodos.Any())
            {
                HashSet<Periodo> periodosProcesados = ProcesarIntersecciones(periodos);
                //Procesamos hasta que no tengamos más periodos que intersectar
                while (periodosProcesados.Any())
                {
                    periodos.UnionWith(periodosProcesados);
                    periodosProcesados = ProcesarIntersecciones(periodos);
                }
                EscribirResultado(periodos);
                Console.WriteLine("Pulse cualquier tecla para finalizar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("No existen conjuntos que procesar, salimos del programa");
            }
        }

        private static void EscribirResultado(HashSet<Periodo> periodos)
        {
            SortedSet<Periodo> resultado = new SortedSet<Periodo>(Comparer<Periodo>.Create((a1, a2) => TimeSpan.Parse(a1.Ini).CompareTo(TimeSpan.Parse(a2.Ini))));
            Console.WriteLine("Periodos tras procesar el fichero");

            //Pasamos a sortedSet para ordenar el resultado por Ini
            foreach (var item in periodos)
            {
                resultado.Add(item);
            }

            try
            {
                StringBuilder salida = new StringBuilder();
                string fichero_salida = ConfigurationManager.AppSettings["RutaEjecucion"] + string.Format("out_Ejercicio1_{0:yyyyMMddhhmmss}.csv", DateTime.Now);
                char sp = ConfigurationManager.AppSettings["Separador"].FirstOrDefault();
                foreach (var item in resultado)
                {
                    Console.WriteLine("\tPeriodo -> Id : {0} | Ini: {1} | Fin: {2}", item.Id, item.Ini, item.Fin);
                    salida.AppendLine(string.Join(sp, item.Id, item.Ini, item.Fin));
                }
                File.WriteAllText(fichero_salida, salida.ToString());
                Console.WriteLine("Fichero escrito con éxito: " + fichero_salida);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al escribir el fichero");
            }
        }

        private static HashSet<Periodo> ProcesarIntersecciones(HashSet<Periodo> periodos)
        {
            HashSet<Periodo> salida = new HashSet<Periodo>();
            List<Periodo> copia = periodos.ToList();
            foreach (var item in periodos)
            {
                //Se elimina el propio elemento para evitar iterar sobre el mismo                
                copia.Remove(item);
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


        private static HashSet<Periodo> LeerFichero(string rutaCompleta)
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
