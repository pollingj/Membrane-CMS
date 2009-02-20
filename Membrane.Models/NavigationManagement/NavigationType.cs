using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.NavigationManagement;

namespace Membrane.Models.NavigationManagement
{
    [ActiveRecord]
    public class NavigationType : ModelBase, INavigationType
    {
        private string name;
        private IList<Navigation> navigationItems;
        private bool isLocked;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasMany(typeof(Navigation))]
		public virtual IList<Navigation> NavigationItems
        {
            get { return navigationItems; }
            set { navigationItems = value; }
        }

        [Property]
		public virtual bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
    }
}