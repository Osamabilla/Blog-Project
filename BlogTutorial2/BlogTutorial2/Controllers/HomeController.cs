using BlogTutorial2.Data;
using BlogTutorial2.Data.FileManager;
using BlogTutorial2.Data.Repository;
using BlogTutorial2.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Controllers
{



    public class HomeController : Controller
    {

        //Interface Repository is where we created the functions to interact with the database
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(
            IRepository repo,
            IFileManager fileManager
            )
        {
            _repo = repo;
            _fileManager = fileManager;
        }


        public IActionResult Index() => View(_repo.GetAllPosts());


        //public IActionResult Index()
        //{
        //    var posts = _repo.GetAllPosts();
        //    return View(posts);
        //}


        public IActionResult Post(int id) => View(_repo.getPost(id));
        


        //public IActionResult Post(int id)
        //{
        //    var post = _repo.getPost(id);



        //    return View(post);
        //}


        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image) => new FileStreamResult(_fileManager.ImageStream(image), $"image/{image.Substring(image.LastIndexOf('.') + 1)}");


        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{

        //    var mime = image.Substring(image.LastIndexOf('.') + 1);
        //    return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        //}


    }
}
