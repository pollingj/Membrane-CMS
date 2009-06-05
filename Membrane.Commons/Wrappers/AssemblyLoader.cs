using System;
using System.Reflection;
using Membrane.Commons.Wrappers.Interfaces;

namespace Membrane.Commons.Wrappers
{
	public class AssemblyLoader : IAssemblyLoader
	{
		public Assembly GetExecutingAssembly()
		{
			return Assembly.GetExecutingAssembly();
		}

		public AssemblyName GetAssemblyName(string assemblyFile)
		{
			return AssemblyName.GetAssemblyName(assemblyFile);
		}

		public Assembly Load(byte[] bytes)
		{
			return Assembly.Load(bytes);
		}

		public Assembly[] GetCurrentDomainAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies();
		}
	}
}