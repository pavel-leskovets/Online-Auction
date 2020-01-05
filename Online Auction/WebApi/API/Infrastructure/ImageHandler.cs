using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Infrastructure
{
    public static class ImageHandler
    {
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
