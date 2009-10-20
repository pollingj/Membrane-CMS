using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Membrane.Commons.Wrappers;
using Membrane.Commons.Wrappers.Interfaces;
using Spark;
using Spark.Compiler;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace Membrane.Commons.SparkExtensions
{
	public class MembraneSparkExtensionsFactory : ISparkExtensionFactory
	{
		private IAssemblyLoader assemblyLoader;

		public MembraneSparkExtensionsFactory()
		{
			assemblyLoader = new AssemblyLoader();
		}
		public ISparkExtension CreateExtension(VisitorContext context, ElementNode node)
		{
			var tags = GetApplicableTags(node);
			if (tags.Any())
			{
				return new MembraneSparkExtension(node, tags);
			}
			return null;
		}

		private IEnumerable<ITag> GetApplicableTags(ElementNode node)
		{
			var result = new List<ITag>();
			//result.AddRange(TagSpecifications.GetMatching(node));
			var pluginTags = getPluginTags();
			var foundTag = pluginTags.Where(t => t.GetType().Name.ToLower() == node.Name.ToLower()).FirstOrDefault();
			if (foundTag != null)
			{
				result.Add(foundTag);
			}

			return result;
		}

		private IEnumerable<ITag> getPluginTags()
		{
			var fileSystem = new FileSystem();
			var pluginFilePaths = fileSystem.GetFiles(@"D:\Projects\Membrane\Membrane-CMS\Membrane.Plugins\bin", "*.dll");
			var tags = new List<ITag>();
			foreach (var pluginFilePath in pluginFilePaths)
			{
				var pluginAssembly = getAssembly(pluginFilePath);

				if (pluginAssembly != assemblyLoader.GetExecutingAssembly())
				{
					try
					{
						var tagTypes = pluginAssembly.GetTypes().Where(t => typeof(ITag).IsAssignableFrom(t)).ToList();

						if (tagTypes.Count > 0)
						{
							//pluginAssemblies.Add(pluginAssembly);

							foreach (var tag in tagTypes)
							{
								var pluginTag = (ITag)Activator.CreateInstance(tag);
								tags.Add(pluginTag);
							}
						}
					}
					catch (ReflectionTypeLoadException)
					{
						//There was a reflection error, ignore for now but probably need to at least log this info
					}
				}
			}

			return tags;
		}

		private Assembly getAssembly(string fileName)
		{
			var assemblies = assemblyLoader.GetCurrentDomainAssemblies();
			Assembly foundAssembly = null;
			var fileSystem = new FileSystem();
			foreach (var assembly in assemblies)
			{
				if (assembly.FullName == assemblyLoader.GetAssemblyName(fileName).FullName)
				{
					foundAssembly = assembly;
					break;
				}
			}

			if (foundAssembly == null)
			{
				var assemblyBytes = fileSystem.ReadAllBytes(fileName);
				foundAssembly = assemblyLoader.Load(assemblyBytes);
			}

			return foundAssembly;
		}
	}

	public interface ITag
	{
		string Render(ElementNode node, IList<Node> body);
	}
}