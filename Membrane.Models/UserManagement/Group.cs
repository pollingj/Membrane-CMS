using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.UserManagement;

namespace Membrane.Models.UserManagement
{
    /// <summary>
    /// Model to handle user group information
    /// </summary>
    [ActiveRecord]
    public class Group : ModelBase, IGroup
    {
        private string name;
        private IList<User> users;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [HasAndBelongsToMany(typeof(User), Table = "UserGroups", ColumnKey = "GroupId", ColumnRef = "UserId")]
        public IList<User> Users
        {
            get { return users; }
            set { users = value; }
        }
    }
}