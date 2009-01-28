
using Membrane.Models.Content;

namespace Membrane.Models.Interfaces.Content
{
    public interface IBaseBlog : IModelBase
    {
        string Title { get; set; }
        string Url { get; set; }
        Template Template { get; set; }
        //string Content { get; set; }
        /* TODO: Maybe have these in a separate auditing section */
        // DateTime Created { get; set; }
        //DateTime Modified { get; set; }

    }
}