using System.Collections.Generic;

namespace Membrane.Commons.FormGeneration.Services.Interfaces
{
	public interface IPropertyReaderService<T>
	{
		void ReadViewModelProperties();
		IList<FormField> FormFields { get; set; }
	}
}