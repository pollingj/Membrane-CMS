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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Property (Length = 300)]
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        [Property("`Order`", NotNull = true)]
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        [BelongsTo("ParentId")]
        public Navigation ParentNode
        {
            get { return parentNode; }
            set { parentNode = value; }
        }

        [BelongsTo("TypeId")]
        public NavigationType NavigationType
        {
            get { return navigationType; }
            set { navigationType = value; }
        }
    }
}