using System.Collections.Generic;

namespace Membrane.Models.Interfaces.Content
{
    public interface ITemplate : IModelBase
    {
        string Name { get; set; }
        string Controller { get; set; }
        string Brail { get; set; }
        IList<PropertyBag> PropertyBags { get; set; }
    }
}