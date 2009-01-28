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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Property (NotNull = true, Length = 100), ValidateNonEmpty]
        public string Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        [Property (NotNull = true, Length = 100), ValidateNonEmpty]
        public string Brail
        {
            get { return brail; }
            set { brail = value; }
        }

        [HasAndBelongsToMany(typeof(PropertyBag), Table = "TemplatePropertyBags", ColumnKey = "TemplateId", ColumnRef = "PropertyBagId")]
        public IList<PropertyBag> PropertyBags
        {
            get { return propertyBags; }
            set { propertyBags = value; }
        }
    }
}