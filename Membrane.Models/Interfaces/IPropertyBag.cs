namespace Membrane.Models.Interfaces
{
    public interface IPropertyBag : IModelBase
    {
        string Name { get; set; }
        // TODO: Add the query info (or FindAll() etc idea)
        // need to consider DetachedCriteria
    }
}