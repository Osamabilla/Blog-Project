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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }


        //This actionresult gets the form values of Post
        [HttpGet]
        public IActionResult Edit()
        {
            return View(new Post());
        }

        //This actionresult takes in the model Post
        [HttpPost]
        public IActionResult Edit(Post post )
        {
            return RedirectToAction("Index");
        }

    }
}
