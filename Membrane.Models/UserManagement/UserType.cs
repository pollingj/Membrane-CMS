using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.UserManagement;

namespace Membrane.Models.UserManagement
{
    /// <summary>
    /// UserTypes are different to groups in that there are currently only 3 user types:
    /// 1. Administrators (often the team of developers)
    /// 2. Editors
    /// 3. Site (this is for creating password protected areas on the public facing site)
    /// </summary>
    [ActiveRecord]
    public class UserType : ModelBase, IUserType
    {
        private string name;
        private IList<User> users;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasMany(typeof(User))]
        public IList<User> Users
        {
            get { return users; }
            set { users = value; }
        }
    }
}