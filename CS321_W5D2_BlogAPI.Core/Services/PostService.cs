using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;
        }

        public Post Add(Post newPost)
        {
            // TODO: Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            var currentUserId = _userService.CurrentUserId;
            //     You may have to retrieve the blog in order to check user id
            var blog = _blogRepository.Get(newPost.BlogId);
            // TODO: assign the current date to DatePublished
            if (blog.UserId == currentUserId)
            {
                //Models
                newPost.DatePublished = DateTime.Now;
                _postRepository.Add(newPost);
                return newPost;
            }
            else
                throw new ApplicationException("Not allowed to post to another users blog.");
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            //     Use the _userService to get the current users id.
            var currentUserId = _userService.CurrentUserId;
            //     You may have to retrieve the blog in order to check user id
            var blog = _blogRepository.Get(id);
            // TODO: prevent user from deleting from a blog that isn't theirs
            if (blog.UserId == currentUserId)
            {
                _postRepository.Remove(id);
            }
            else
                throw new ApplicationException("Not allowed to delete another users blog.");
        }

        public Post Update(Post updatedPost)
        {
            // TODO: prevent user from updating a blog that isn't theirs
            var currentUserId = _userService.CurrentUserId;
            var blog = _blogRepository.Get(updatedPost.BlogId);

            if (blog.UserId == currentUserId)
            {
                var newPost = _postRepository.Update(updatedPost);
                return newPost;
            }
            else
                throw new ApplicationException("Not allowed to update another users blog.");
        }

    }
}
