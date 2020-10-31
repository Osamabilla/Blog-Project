using BlogTutorial2.Data;
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

        private AppDbContext _ctx;

        public HomeController(AppDbContext ctx)
        {
            _ctx = ctx;
        }



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
        public async Task<IActionResult> Edit(Post post )
        {

            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
