using BlogTutorial2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Data.Repository
{
    //This is basically just creating the functions we need, we implement them in the repository file
    public interface IRepository
    {
        Post getPost(int id);
        List<Post> GetAllPosts(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);


        Task<bool> SaveChangesAsync();

    }
}
