using System.Collections.Generic;
using Membrane.Entities;

namespace Membrane.Core.Services.Interfaces
{
	public interface IContentService
	{
		ICollection<ContentElement> GetCurrentElements(string modelName);
		string GetContentModelName(string contentType);
		object GetElementItem(string contentType, int id);
	}
}