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
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasMany(typeof(Navigation))]
        public IList<Navigation> NavigationItems
        {
            get { return navigationItems; }
            set { navigationItems = value; }
        }

        [Property]
        public bool IsLocked
        {
            get { return isLocked; }
            set { isLocked = value; }
        }
    }
}