using System;
using System.Reflection;

namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IAssemblyLoader
	{
		Assembly GetExecutingAssembly();
		AssemblyName GetAssemblyName(string assemblyFile);
		Assembly Load(byte[] bytes);
		Assembly[] GetCurrentDomainAssemblies();
	}
}