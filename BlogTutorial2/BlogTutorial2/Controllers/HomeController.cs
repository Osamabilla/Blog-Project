using BlogTutorial2.Data;
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

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }


        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id)
        {
            var post = _repo.getPost(id);



            return View(post);
        }


    }
}
