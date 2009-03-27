using System;
using System.Security.Principal;
using Membrane.Entities;
using Membrane.Entities.Enums;

namespace Membrane.Core.DTOs
{
	public class AuthenticatedUserDTO : IPrincipal, IIdentity
	{
		public Guid Id { get; set; }
		public UserType Type { get; set; }

		public bool IsInRole(string role)
		{
			return role == Type.ToString();
		}

		public virtual IIdentity Identity
		{
			get { return this; }
		}

		public virtual string Name
		{
			get { return Id.ToString(); }
		}

		public virtual string AuthenticationType
		{
			get { return string.Empty; }
		}

		public virtual bool IsAuthenticated
		{
			get { return true; }
		}
	}
}