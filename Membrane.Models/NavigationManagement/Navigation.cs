using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.NavigationManagement;

namespace Membrane.Models.NavigationManagement
{
    [ActiveRecord]
    public class Navigation : ModelBase, INavigation
    {
        private string name;
        private string link;
        private int order;
        private Navigation parentNode;
        private NavigationType navigationType;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Property (Length = 300)]
		public virtual string Link
        {
            get { return link; }
            set { link = value; }
        }

        [Property("`Order`", NotNull = true)]
		public virtual int Order
        {
            get { return order; }
            set { order = value; }
        }

        [BelongsTo("ParentId")]
		public virtual Navigation ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        [BelongsTo("TypeId")]
		public virtual NavigationType NavigationType
        {
            get { return navigationType; }
            set { navigationType = value; }
        }
    }
}