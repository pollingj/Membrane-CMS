using Castle.MonoRail.Framework;
using Membrane.Plugins.Blog.Services;

namespace Membrane.Plugins.Blog.Controllers
{
    public class BlogController : SmartDispatcherController
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [DefaultAction]
        public void Post()
        {
            PropertyBag["post"] = blogService.RetrievePostByUri(Action);

            RenderView("Post");
        }
    }
}