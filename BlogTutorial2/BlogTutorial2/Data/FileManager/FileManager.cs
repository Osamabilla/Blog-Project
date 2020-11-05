using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoSauce.MagicScaler;

namespace BlogTutorial2.Data.FileManager
{
    //Here we inherit the interface IFileManager which contains the functions to handle the files
    public class FileManager : IFileManager
    {
        private string _imagePath;

        //Here we inject the IConfiguration, which is the appsettings.js, because in that file lies our image path. the path to the images so they can be saved
        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
        }

        public FileStream ImageStream(string image)
        {
            return new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);
        }

        public async Task<string> SaveImage(IFormFile image)
        {

            try
            {


                var save_path = Path.Combine(_imagePath);

                if (!Directory.Exists(_imagePath))
                {
                    Directory.CreateDirectory(save_path);
                }

                //Internet Explorer Error
                //var fileName = image.FileName;

                //var mime gets the code of the image (jpg, png, etc), filename is the name we give to the image, and add to it the mime.
                var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
                var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

                using (var fileStream = new FileStream(Path.Combine(save_path, fileName), FileMode.Create))
                {
                    
                    MagicImageProcessor.ProcessImage(image.OpenReadStream(), fileStream, ImageOptions());
                }


             

                return fileName;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }

        }

        private ProcessImageSettings ImageOptions() => new ProcessImageSettings
        {



            Width = 800,
            Height = 500,
            SaveFormat = FileFormat.Jpeg,
            JpegQuality = 100,
            ResizeMode = CropScaleMode.Max,
            JpegSubsampleMode = ChromaSubsampleMode.Subsample420
        };



    }
}
