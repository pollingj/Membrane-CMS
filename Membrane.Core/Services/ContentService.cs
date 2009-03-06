using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Mapping;
using Membrane.Commons.Persistence;
using Membrane.Core.Services.Interfaces;
using Membrane.Entities;
using System.Linq;

namespace Membrane.Core.Services
{
	public class ContentService : IContentService
	{
		private readonly IRepository<ContentType> contentTypeRepository;
		private readonly IRepository<ContentElement> contentElementRepository;

		#region Reflection Work

		private Type GetReflectedType(string modelName)
		{
			// Hard code assembly for now.  This needs to be set in the db later
			var assembly = Assembly.LoadFrom(@"C:\Membrane\Membrane.TestSite.Entities\bin\Debug\Membrane.TestSite.Entities.dll");
			Type foundType = null;

			foreach (var type in assembly.GetTypes())
			{
				if (type.Name == modelName)
				{
					foundType = type;
					break;
				}
			}

			return foundType;
		}

		private ContentType GetContentType(Type type)
		{
			var typeQuery = contentTypeRepository.AsQueryable().Where(x => x.Model == type.Name);
			return contentTypeRepository.FindOne(typeQuery);
		}

		#endregion

		public ContentService(IRepository<ContentType> contentTypeRepository, IRepository<ContentElement> contentElementRepository)
		{
			this.contentTypeRepository = contentTypeRepository;
			this.contentElementRepository = contentElementRepository;
		}

		public ICollection<ContentElement> GetCurrentElements(string modelName)
		{
			// Find the Content Elements with this type
			var type = GetReflectedType(modelName);
			var elementQuery = contentElementRepository.AsQueryable().Where(x => x.Type == GetContentType(type));

			return contentElementRepository.Find(elementQuery);
		}


		public string GetContentModelName(string contentType)
		{
			throw new System.NotImplementedException();
		}

		public Dictionary<string, object> GetElementItem(string modelName, int id)
		{
			var values = new Dictionary<string, object>();
			var type = GetReflectedType(modelName);

			// This gives us an object array
			var returnedData = contentElementRepository.FindOne(CreateReflectedQuery(type), type);

			var count = 0;

			// Match the returnedData object array up to the Properties in the reflected type
			foreach (var info in type.GetProperties())
			{
				if (info.DeclaringType == type)
				{
					if (info.PropertyType.FullName.IndexOf("Collection") > -1)
					{
						// Build Query
						values.Add(info.PropertyType.GetGenericArguments()[0].Name, new object[] { 1, 2 });
					}
					else
					{
						values.Add(info.Name, returnedData[count]);
						count++;
					}
				}

			}

			return values;
		}


		/// <summary>
		/// Generates the basic select statement on the fly.  This is based on the properties found within the 
		/// reflected entity
		/// </summary>
		/// <param name="type">The reflected entity type</param>
		/// <returns>Query string</returns>
		private string CreateReflectedQuery(Type type)
		{
			StringBuilder query = new StringBuilder("SELECT ");
			
			foreach (var info in type.GetProperties())
			{
				if (info.DeclaringType == type && info.PropertyType.FullName.IndexOf("Collection") == -1)
					query.AppendFormat("{0}.{1},", type.Name, info.Name);
			}
			
			// Remove last comma
			query.Remove(query.Length - 1, 1);

			query.AppendFormat(" FROM {0}", type.Name);

			return query.ToString();
		}


	}
}