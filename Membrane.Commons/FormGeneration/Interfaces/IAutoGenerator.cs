using System.Collections.Generic;

namespace Membrane.Commons.FormGeneration.Interfaces
{
	public interface IAutoGenerator<T>
	{
		void ReadViewModelProperties();
		IList<FormField> FormFields { get; set; }
	}
}