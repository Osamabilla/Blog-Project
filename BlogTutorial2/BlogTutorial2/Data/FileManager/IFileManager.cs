﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Data.FileManager
{
    public interface IFileManager
    {

        FileStream ImageStream(string image);
        Task<string> SaveImage(IFormFile image);

    }
}
