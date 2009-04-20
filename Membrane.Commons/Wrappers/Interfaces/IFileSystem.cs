namespace Membrane.Commons.Wrappers.Interfaces
{
	public interface IFileSystem
	{
		string[] GetFiles(string path, string searchPattern);
		byte[] ReadAllBytes(string path);
	}
}