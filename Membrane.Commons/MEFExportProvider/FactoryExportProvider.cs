using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace Membrane.Commons.MEFExportProvider
{
	public class FactoryExportProvider<T> : ExportProvider
	{
		private readonly IList<FactoryExportDefinition<T>> definitions = new List<FactoryExportDefinition<T>>();

		public FactoryExportProvider(Func<Type, T> resolutionMethod)
			: this(Assembly.GetExecutingAssembly(), resolutionMethod)
		{
		}

		public FactoryExportProvider(Assembly assembly, Func<Type, T> resolutionMethod)
		{
			var interfaces = from t in assembly.GetTypes()
										   where t.IsInterface
										   select t;

			ComposeTypes(interfaces, resolutionMethod);
		}

		public FactoryExportProvider(IEnumerable<Type> types, Func<Type, T> resolutionMethod)
		{
			ComposeTypes(types, resolutionMethod);
		}

		protected override IEnumerable<Export> GetExportsCore(ImportDefinition importDefinition)
		{
			var exports = new List<Export>();
			var constraint = importDefinition.Constraint.Compile();
			var foundExports = from d in definitions
							   where constraint(d)
							   select new Export(d, () => d.ResolutionMethod(d.ServiceType));

			if (importDefinition.Cardinality == ImportCardinality.ZeroOrMore)
				exports = foundExports.ToList();
			else if (foundExports.Count() == 1)
				exports.Add(foundExports.First());

			return exports;
		}

		private void ComposeTypes(IEnumerable<Type> serviceTypes, Func<Type, T> resolutionMethod)
		{
			foreach (var type in serviceTypes)
			{
				var interfaceType = (from i in type.GetInterfaces()
									  where i == typeof(T)
									  select i).SingleOrDefault();

				if (interfaceType != null)
					definitions.Add(new FactoryExportDefinition<T>(type.ToString(), type, resolutionMethod));
			}
		}
	}
}