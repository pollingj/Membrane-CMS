using System;
using System.Collections.Generic;
using System.Text;
using Spark;
using Spark.Compiler;
using Spark.Compiler.ChunkVisitors;
using Spark.Compiler.NodeVisitors;
using Spark.Parser.Markup;

namespace Membrane.Commons.SparkExtensions
{
	public class MembraneSparkExtension : ISparkExtension
	{
		private readonly IEnumerable<ITag> tags;
		private ElementNode node;

		public MembraneSparkExtension(ElementNode node, IEnumerable<ITag> tags)
		{
			this.tags = tags;
			this.node = node;
		}

		public void VisitNode(INodeVisitor visitor, IList<Node> body, IList<Chunk> chunks)
		{
			Render(node, body);			
		}

		public void VisitChunk(IChunkVisitor visitor, OutputLocation location, IList<Chunk> body, StringBuilder output)
		{
			visitor.Accept(body);
			//output = Render(node, body);
			//visitor.Accept(body);
		}

		protected StringBuilder Render(ElementNode elementNode, IList<Node> body)
		{
			var output = new StringBuilder();
			foreach (var tag in tags)
			{
				output.AppendLine(tag.Render(elementNode, body));
			}

			return output;
		}
	}
}