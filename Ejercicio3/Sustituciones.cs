using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ejercicio3
{
    public abstract class Sustituciones
    {
        public int SustitucionesId { get; set; }
        public string Texto { get; set; }
        public int idPadre { get; set; }
    }

    public class SustitucionesCompuestas : Sustituciones, IEnumerable
    {

        private List<Sustituciones> sustituciones = new List<Sustituciones>();
        public void Addsustitucion(Sustituciones sustitucion)
        {
            sustituciones.Add(sustitucion);
        }
        public void Removesustitucion(Sustituciones sustitucion)
        {
            sustituciones.Remove(sustitucion);
        }
        public Sustituciones GetSustitcuion(int index)
        {
            return sustituciones[index];
        }

        public IEnumerator GetEnumerator()
        {
            foreach (Sustituciones sustitucion in sustituciones)
                yield return sustitucion;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class SustitucionesTexto : Sustituciones
    {

    }

    public class SustitucionesTeclado : Sustituciones
    {

    }


}
