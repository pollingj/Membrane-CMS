using System.Collections.Generic;
using Membrane.Commons.SparkExtensions;
using Spark.Parser.Markup;

namespace Membrane.Plugins.News.Tags
{
	public class News : ITag
	{
		public string Render(ElementNode node, IList<Node> body)
		{
			var output = "";
			for(var count = 0; count < 3; count++)
			{
				output += body;
			}

			return output;
		}
	}
}