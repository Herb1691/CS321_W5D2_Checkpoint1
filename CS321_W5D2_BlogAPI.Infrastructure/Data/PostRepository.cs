using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;
        public PostRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
            return _dbContext.Posts
                .Include(p => p.Blog)
                    .ThenInclude(b => b.User)
                .Include(p => p.Comments)
                .SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            return _dbContext.Posts
                .Include(p => p.Blog)
                    .ThenInclude(b => b.User)
                .Include(p => p.Comments)
                .Where(p => p.BlogId == blogId)
                .ToList();
        }

        public Post Add(Post post)
        {
            // TODO: add Post
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return post;
        }

        public Post Update(Post updatedPost)
        {
            // TODO: update Post
            var existingPost = _dbContext.Posts.Find(updatedPost.Id);

            if (existingPost == null) return null;

            _dbContext.Entry(existingPost)
                .CurrentValues
                .SetValues(updatedPost);

            _dbContext.Posts.Update(existingPost);
            _dbContext.SaveChanges();

            return existingPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
            return _dbContext.Posts
                .Include(p => p.Blog)
                    .ThenInclude(b => b.User)
                .Include(p => p.Comments)
                .ToList();
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            var deletePost = _dbContext.Posts.Find(id);

            _dbContext.Posts.Remove(deletePost);
            _dbContext.SaveChanges();
        }

    }
}
