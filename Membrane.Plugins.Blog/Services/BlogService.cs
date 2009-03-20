using System;
using AutoMapper;
using Membrane.Commons;
using Membrane.Commons.Persistence;
using Membrane.Plugins.Blog.DTOs;
using Membrane.Plugins.Blog.Entities;
using Membrane.Plugins.Blog.Queries;

namespace Membrane.Plugins.Blog.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Post> postRepository;

        public BlogService(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public PostDetailsDTO RetrievePostByUri(string uri)
        {
            GuardAgainst.ArgumentNullOrEmpty(uri, "uri");

            if(uri.Contains(" "))
                throw new ArgumentException("The uri for a post cannot contain a space");

            Post post = postRepository.FindOne(new FindPostByUri(uri));
            
            return Mapper.Map<Post, PostDetailsDTO>(post);
        }
    }
}