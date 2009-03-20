using AutoMapper;
using Castle.Windsor;
using Membrane.Commons;
using Membrane.Plugins.Blog.DTOs;
using Membrane.Plugins.Blog.Entities;
using Membrane.Plugins.Blog.Services;

namespace Membrane.Plugins.Blog
{
    public class Plugin : IMembranePlugin
    {
        public string Name
        {
            get { return "Blogs"; }
        }

        public void Initialize()
        {
            Mapper.CreateMap<Post, PostDetailsDTO>();
        }

        public void RegisterComponents(IWindsorContainer container)
        {
            container.AddComponent<IBlogService, BlogService>();
        }
    }
}
