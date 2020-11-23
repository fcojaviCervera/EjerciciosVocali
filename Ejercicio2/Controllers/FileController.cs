using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ejercicio2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            File myFile = new File
            {
                MyFile = file
            };

            if (myFile.MyFile != null && myFile.MyFile.FileName.EndsWith(".mp3"))
            {
                try
                {
                    return StatusCode(200, myFile.ProcesateFile());
                }
                catch (Exception e)
                {
                    return StatusCode(503, e.Message);
                }
            }
            return StatusCode(415, "El fichero adjunto no tiene extension .mp3");

        }

    }
}