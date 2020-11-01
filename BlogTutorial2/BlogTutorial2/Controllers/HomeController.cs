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


        //This actionresult gets the form values of Post
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if(id == null)
                return View(new Post());
            else
            {
                var post = _repo.getPost((int) id);
                return View(post);
            }


        }

        //Hvis post id > 0 then the post already exists and we just update the post
        //hvis post id is not bigger than 0 then the post does not exist, and we create a new post
        [HttpPost]
        public async Task<IActionResult> Edit(Post post )
        {

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);



            if(await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
            {
                return View(post);
            }
        }



        //Function to remove a post. Uses the functions we created in the Repository interface
        //Takes in argument id, which is the id of the post you want to remove
        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}
