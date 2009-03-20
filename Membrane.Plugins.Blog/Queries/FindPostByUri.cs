using System.Linq;
using Membrane.Commons.Persistence;
using Membrane.Plugins.Blog.Entities;

namespace Membrane.Plugins.Blog.Queries
{
    public class FindPostByUri : IQueryCommand<Post>
    {
        private readonly string uri;

        public FindPostByUri(string uri)
        {
            this.uri = uri;
        }

        public IQueryable<Post> Execute(IRepository<Post> repository)
        {
            return repository.AsQueryable().Where(p => p.Uri == uri);
        }
    }
}