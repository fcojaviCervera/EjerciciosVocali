using Ejercicio3.Migrations;
using System;

namespace Ejercicio3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Ejercicio 3\r");
            Console.WriteLine("------------------------\n");            

            bool salir = false;
            while (!salir)
            {
                // Ask the user to choose an option.
                Console.WriteLine("Introduzca un numero con la opcion de la lista:");
                Console.WriteLine("\t1 - Añadir sustitucion de texto");
                Console.WriteLine("\t2 - Añadir sustitucion de teclado");
                Console.WriteLine("\t3 - Añadir sustitucion multiple");
                Console.WriteLine("\t4 - Salir");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Introduzca el texto para la sustitucion de texto");
                        var texto = Console.ReadLine();
                        var stTexto = new SustitucionesTexto()
                        {
                            Texto = texto
                        };
                        AddSustitucion(stTexto);
                        break;
                    case "2":
                        Console.WriteLine("Introduzca el texto para la sustitucion de teclado");
                        var teclado = Console.ReadLine();
                        var stTeclado = new SustitucionesTeclado()
                        {
                            Texto = teclado
                        };
                        AddSustitucion(stTeclado);
                        break;
                    case "3":
                        Console.WriteLine("Introduzca el número de sustituciones que desea añadir");
                        var numero = Console.ReadLine();
                        var stMultiple = new SustitucionesCompuestas();
                        AddSustitucionMultiple(numero, stMultiple);
                        break;
                    case "4":
                        Console.WriteLine("Salimos del programa");
                        salir = true;
                        break;
                }

            }
            

        }

        static Sustituciones AddSustitucion(Sustituciones sustitucion)
        {
            using (var context = new SustitucionesContext())
            {
                context.Sustituciones.Add(sustitucion);
                context.SaveChanges();
                return sustitucion;
            }
        }

        static Sustituciones AddSustitucionMultiple(string numeroMultiple, SustitucionesCompuestas sustitucionesCompuestas)
        {
            using (var context = new SustitucionesContext())
            {
                context.Sustituciones.Add(sustitucionesCompuestas);
                context.SaveChanges();

                for (int i = 0; i < int.Parse(numeroMultiple); i++)
                {
                    Console.WriteLine("Introduzca un numero con la opcion de la lista:");
                    Console.WriteLine("\t1 - Añadir sustitucion de texto");
                    Console.WriteLine("\t2 - Añadir sustitucion de teclado");
                    Console.WriteLine("\t3 - Añadir sustitucion multiple");

                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("Introduzca el texto para la sustitucion de texto");
                            var texto = Console.ReadLine();
                            var stTexto = new SustitucionesTexto()
                            {
                                Texto = texto,
                                idPadre = sustitucionesCompuestas.SustitucionesId
                            };
                            sustitucionesCompuestas.Addsustitucion(AddSustitucion(stTexto));
                            break;
                        case "2":
                            Console.WriteLine("Introduzca el texto para la sustitucion de teclado");
                            var teclado = Console.ReadLine();
                            var stTeclado = new SustitucionesTeclado()
                            {
                                Texto = teclado,
                                idPadre = sustitucionesCompuestas.SustitucionesId
                            };
                            sustitucionesCompuestas.Addsustitucion(AddSustitucion(stTeclado));
                            break;
                        case "3":
                            Console.WriteLine("Introduzca el número de sustituciones que desea añadir");
                            var numero = Console.ReadLine();
                            var stMultiple = new SustitucionesCompuestas()
                            {
                                idPadre = sustitucionesCompuestas.SustitucionesId
                            };                            
                            sustitucionesCompuestas.Addsustitucion(AddSustitucionMultiple(numero, stMultiple));
                            break;
                    }
                }
                context.SaveChanges();

                return sustitucionesCompuestas;
            }
        }

    }
}
