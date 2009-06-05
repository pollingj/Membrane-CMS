using System.IO;
using Membrane.Commons.Wrappers.Interfaces;

namespace Membrane.Commons.Wrappers
{
	public class FileSystem : IFileSystem
	{
		public string[] GetFiles(string path, string searchPattern)
		{
			return Directory.GetFiles(path, searchPattern);
		}

		public byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}
	}
}