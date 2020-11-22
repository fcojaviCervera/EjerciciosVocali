using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ejercicio2
{
    public class File
    {

        private static readonly string[] Phrases = new[]
        {
            "Texto generico 1", "Texto generico 2", "Texto genérico 3", "Texto genérico 4"
        };

        public IFormFile MyFile;

        public string ProcesateFile()
        {

            Random random = new Random();
            int porcentajeError = random.Next(0, 100);

            if (MyFile != null && porcentajeError>5)
            {
                return Phrases[(MyFile.Length + MyFile.FileName.Length) % 4];
            }
            else
            {
                throw new Exception("Error generico. Valor no encontrado");
            }
        }


    }
}
