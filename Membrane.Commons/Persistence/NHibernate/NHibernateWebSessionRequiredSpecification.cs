using System;

namespace Membrane.Commons.Persistence.NHibernate
{
	public class NHibernateWebSessionRequiredSpecification : ISpecification<Uri>
	{
		public bool IsSatisfiedBy(Uri candidate)
		{
			return candidate.AbsolutePath.ToLower().EndsWith(".rails");
		}

		public ISpecification<Uri> And(ISpecification<Uri> other)
		{
			throw new NotImplementedException();
		}

		public ISpecification<Uri> Or(ISpecification<Uri> other)
		{
			throw new NotImplementedException();
		}

		public ISpecification<Uri> Not()
		{
			throw new NotImplementedException();
		}
	}
}