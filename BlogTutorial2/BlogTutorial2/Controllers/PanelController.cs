using BlogTutorial2.Data.FileManager;
using BlogTutorial2.Data.Repository;
using BlogTutorial2.Models;
using BlogTutorial2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public PanelController(
            IRepository repo,
            IFileManager fileManager
            )
        {
            _repo = repo;
            _fileManager = fileManager;
        }


        public IActionResult Index()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Post(int id) => View(_repo.getPost(id));
        public IActionResult Panel()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }

        public IActionResult Private()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }



        //This actionresult gets the form values of Post
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id == null)
                return View(new PostViewModel());
            else
            {
                var post = _repo.getPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    Description = post.Description,
                    Category = post.Category,
                    Tags = post.Tags
                });
            }


        }

        //Hvis post id > 0 then the post already exists and we just update the post
        //hvis post id is not bigger than 0 then the post does not exist, and we create a new post
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {

            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Category = vm.Category,
                Tags = vm.Tags,
                Image = await _fileManager.SaveImage(vm.Image)
            };




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
