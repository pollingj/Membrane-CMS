using System.Reflection;

namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IAssemblyLoader
	{
		string FullName { get; }
		IAssemblyLoader GetExecutingAssembly();
		AssemblyName GetAssemblyName(string assemblyFile);
		IAssemblyLoader Load(byte[] bytes);
	}
}