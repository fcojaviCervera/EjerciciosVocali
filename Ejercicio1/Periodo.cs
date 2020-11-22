using System;
using System.Text.RegularExpressions;

namespace Ejercicio1
{
    class Periodo : IComparable
    {

        public string Id { get; set; }
        public string Ini { get; set; }
        public string Fin { get; set; }

        public Periodo(string _id, string _Ini, string _Fin)
        {
            Id = _id;
            Ini = _Ini;
            Fin = _Fin;
        }

        public Periodo()
        {
        }

        public bool IsValid()
        {
            //Expresion regular para validar la hora
            Regex validHour = new Regex(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            return
                    !string.IsNullOrEmpty(Id) &&
                    !string.IsNullOrEmpty(Ini) &&
                    !string.IsNullOrEmpty(Fin) &&
                    validHour.IsMatch(Ini) &&
                    validHour.IsMatch(Fin) &&
                    TimeSpan.Parse(Ini) < TimeSpan.Parse(Fin);
        }

        public override bool Equals(object obj)
        {
            return obj is Periodo && Equals((Periodo)obj);
        }

        public bool Equals(Periodo obj)
        {
            return (Ini.Equals(obj.Ini) && Fin.Equals(obj.Fin)) || Id.Equals(obj.Id);
        }

        public override int GetHashCode()
        {
            return Ini.GetHashCode()+Fin.GetHashCode();
        }

        /***
         * Devuelve el periordo interseccion de los dos periodos, si lo hubiera. Si no hay intersección, devuelve nulo
         */
        public Periodo Intersect(Periodo obj)
        {
            TimeSpan timeSpanIni = TimeSpan.Parse(Ini);
            TimeSpan timeSpanFin = TimeSpan.Parse(Fin);
            TimeSpan objtimeSpanIni = TimeSpan.Parse(obj.Ini);
            TimeSpan objtimeSpanFin = TimeSpan.Parse(obj.Fin);
            TimeSpan minuto = TimeSpan.Parse("00:01");
            
            //Mismos periodos
            if (timeSpanIni == objtimeSpanIni && timeSpanFin == objtimeSpanFin)
            {
                Console.WriteLine("\tPeriodos iguales. No intersectan. -> Id : {0} y {3} | Ini: {1} | Fin: {2}", Id, obj.Ini, Fin, obj.Id);
                //Guardamos el id y nos quedamos con el 
                Id += obj.Id;
                return null;
            }
            else if (timeSpanIni == objtimeSpanIni && timeSpanFin <= objtimeSpanFin)
            {
                Console.WriteLine("\tPeriodos con mismo comienzo.  -> Id : {0} | Ini: {1} | Fin: {2}", Id + obj.Id, Ini, obj.Fin);
                Id += obj.Id;
                obj.Ini = timeSpanFin.Add(minuto).ToString(@"hh\:mm");
                return null;
            }
            else if (timeSpanIni<=objtimeSpanIni && timeSpanFin > objtimeSpanFin)
            {
                Console.WriteLine("\tPeriodo incluido dentro de otro. Separamos en 3 periodos -> Id : {0} | Ini: {1} | Fin: {2}", Id + obj.Id, obj.Ini, obj.Fin);
                Periodo p = new Periodo(Id + obj.Id, obj.Ini, obj.Fin);

                //Reutilizamos Id para indicar que se trata del mismo periodo
                obj.Id = Id+"\'";
                obj.Fin = Fin;
                Fin = objtimeSpanIni.Subtract(minuto).ToString(@"hh\:mm");
                obj.Ini = objtimeSpanFin.Add(minuto).ToString(@"hh\:mm");                

                return p;
            }
            else if (timeSpanIni < objtimeSpanIni && timeSpanFin > objtimeSpanIni)
            {
                Console.WriteLine("\tPeriodo interseccion-> Id : {0} | Ini: {1} | Fin: {2}", Id + obj.Id, obj.Ini, Fin);
                Periodo p = new Periodo(Id + obj.Id, obj.Ini, Fin);
                Fin = objtimeSpanIni.Subtract(minuto).ToString(@"hh\:mm");
                obj.Ini = timeSpanFin.Add(minuto).ToString(@"hh\:mm");
                return p;
            }
            else if (timeSpanIni < objtimeSpanFin && timeSpanFin > objtimeSpanFin)
            {
                Console.WriteLine("\tPeriodo interseccion-> Id : {0} | Ini: {1} | Fin: {2}", obj.Id + Id, Ini, obj.Fin);
                Periodo p = new Periodo(obj.Id + Id, Ini, obj.Fin);
                Ini = objtimeSpanFin.Add(minuto).ToString(@"hh\:mm");
                obj.Fin = timeSpanIni.Subtract(minuto).ToString(@"hh\:mm");
                return p;
            }            

            return null;
        }

        public int CompareTo(object obj)
        {
            return Ini.CompareTo(obj);
        }
    }
}
