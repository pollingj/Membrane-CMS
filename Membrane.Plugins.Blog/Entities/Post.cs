using System;
using Membrane.Commons.Persistence;

namespace Membrane.Plugins.Blog.Entities
{
    public class Post : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
        public virtual string Uri { get; set; }
    }
}