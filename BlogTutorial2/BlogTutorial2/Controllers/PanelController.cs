using BlogTutorial2.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        public PanelController(IRepository repo)
        {
            _repo = repo;
        }


        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }



        //This actionresult gets the form values of Post
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
                return View(new Models.Post());
            else
            {
                var post = _repo.getPost((int)id);
                return View(post);
            }


        }

        //Hvis post id > 0 then the post already exists and we just update the post
        //hvis post id is not bigger than 0 then the post does not exist, and we create a new post
        [HttpPost]
        public async Task<IActionResult> Edit(Models.Post post)
        {

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);



            if (await _repo.SaveChangesAsync())
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
