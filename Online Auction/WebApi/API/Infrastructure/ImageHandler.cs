using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    /// <summary>
    /// Class which handles received images.
    /// </summary>
    public static class ImageHandler
    {
        /// <summary>
        /// Method for uploading image to wwwroot/upload directory
        /// </summary>
        /// <param name="image">Received image.</param>
        /// <param name="_environment">Web Host Environment</param>
        /// <returns>Path where image is stored.</returns>
        public static string SaveImage(IFormFile image, IWebHostEnvironment _environment)
        {
            var randomName = $"{Guid.NewGuid()}." + image.ContentType.Substring(6);
            string path = _environment.WebRootPath + "\\Upload\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fileStream = System.IO.File.Create(path + randomName))
            {
                image.CopyTo(fileStream);
                fileStream.Flush();
                return "\\Upload\\" + randomName;
            }
        }

    }
}
