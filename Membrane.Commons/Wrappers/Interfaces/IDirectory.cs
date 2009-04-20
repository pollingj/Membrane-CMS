namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IDirectory
	{
		string[] GetFiles(string path, string searchPattern);
	}
}