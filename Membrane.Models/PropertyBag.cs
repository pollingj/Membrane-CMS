using Castle.ActiveRecord;
using Membrane.Models.Interfaces;

namespace Membrane.Models
{
    [ActiveRecord]
    public class PropertyBag : ModelBase, IPropertyBag
    {
        private string name;

        [Property]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}