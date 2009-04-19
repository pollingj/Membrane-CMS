using System.Collections.Generic;

namespace Membrane.Commons.FormGeneration.Interfaces
{
	public interface IPropertyReaderService<T>
	{
		void ReadViewModelProperties();
		IList<FormField> FormFields { get; set; }
	}
}