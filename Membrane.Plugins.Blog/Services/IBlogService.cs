using Membrane.Plugins.Blog.DTOs;

namespace Membrane.Plugins.Blog.Services
{
    public interface IBlogService
    {
        PostDetailsDTO RetrievePostByUri(string uri);
    }
}