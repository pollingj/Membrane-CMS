using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.Content;

namespace Membrane.Models.Content
{
    [ActiveRecord]
    public class Template : ModelBase, ITemplate
    {
        private string name;
        private string controller;
        private string brail;
        private IList<PropertyBag> propertyBags;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Property (NotNull = true, Length = 100), ValidateNonEmpty]
		public virtual string Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        [Property (NotNull = true, Length = 100), ValidateNonEmpty]
		public virtual string Brail
        {
            get { return brail; }
            set { brail = value; }
        }

        [HasAndBelongsToMany(typeof(PropertyBag), Table = "TemplatePropertyBags", ColumnKey = "TemplateId", ColumnRef = "PropertyBagId")]
		public virtual IList<PropertyBag> PropertyBags
        {
            get { return propertyBags; }
            set { propertyBags = value; }
        }
    }
}