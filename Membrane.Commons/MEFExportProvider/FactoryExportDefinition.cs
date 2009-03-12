using System;
using System.ComponentModel.Composition.Primitives;

namespace Membrane.Commons.MEFExportProvider
{
	public class FactoryExportDefinition<T> : ExportDefinition
	{
		private readonly string contractName;

		public FactoryExportDefinition(string contractName, Type type, Func<Type, T> resolutionMethod)
		{
			this.contractName = contractName;
			ResolutionMethod = resolutionMethod;
			ServiceType = type;
		}

		public override string ContractName
		{
			get { return contractName; }
		}

		public Type ServiceType { get; private set; }
		public Func<Type, T> ResolutionMethod { get; private set; }
	}
}