namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IAssembly
	{
		string FullName { get; set; }
		IAssembly GetExecutingAssembly();
	}
}