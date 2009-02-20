using System.Collections.Generic;
using Castle.ActiveRecord;
using Castle.Components.Validator;
using Membrane.Models.Interfaces.UserManagement;

namespace Membrane.Models.UserManagement
{
    [ActiveRecord]
    public class User : ModelBase, IUser
    {
        private string name;
        private UserType type;
        private IList<Group> groups;
        private string username;
        private string email;
    	private string password;

        [Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        [BelongsTo("TypeId")]
		public virtual UserType Type
        {
            get { return type; }
            set { type = value; }
        }

        [HasAndBelongsToMany(typeof(Group), Table = "UserGroups", ColumnKey = "UserId", ColumnRef = "GroupId")]
		public virtual IList<Group> Groups
        {
            get { return groups; }
            set { groups = value; }
        }

        [Property (NotNull = true, Length = 200), ValidateNonEmpty, ValidateIsUnique]
		public virtual string Username
        {
            get { return username; }
            set { username = value; }
        }

        [Property (NotNull = true, Length = 200), ValidateNonEmpty, ValidateIsUnique, ValidateEmail]
		public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

		[Property (NotNull = true, Length = 200), ValidateNonEmpty]
		public virtual string Password
    	{
			get { return password; }
			set { password = value; }
    	}

    }
}