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
			var assembly = Assembly.LoadFrom(@"C:\Membrane\Membrane\bin\Membrane.Entities.dll");
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

			return contentElementRepository.Find(elementQuery);;
			//return query.List(); 
		}


		public string GetContentModelName(string contentType)
		{
			throw new System.NotImplementedException();
		}

		public object GetElementItem(string modelName, int id)
		{
			/*
			var type = GetReflectedType(modelName);
			//var contentType = GetContentType(type);
			var query = CreateReflectedQuery(type, id);


			return contentElementRepository.FindOne(query, type);*/
			return null;
		}

		/*private string CreateReflectedQuery(Type type, int id)
		{
			StringBuilder query = new StringBuilder("SELECT ");
			IList<string> tableNames = new List<string>();
			foreach (var info in type.GetProperties())
			{
				if (info.PropertyType.FullName.IndexOf("System.") > -1)
					query.AppendFormat("{0}.{1},", type.Name, info.Name);
				else
				{
					query.AppendFormat("{0}.{1},", info.PropertyType.Name, info.Name);
					tableNames.Add(info.PropertyType.Name);
				}
			}

			//FluentNHibernate.Utils.ReflectionHelper helper = new FluentNHibernate.Utils.ReflectionHelper();
			//FluentNHibernate.Mapping.
			

			// Remove last comma
			query.Remove(query.Length - 1, 1);

			query.AppendFormat(" FROM {0},", type.Name);
			foreach (var table in tableNames)
			{
				query.AppendFormat("{0},", table);
			}

			// Remove last comma
			query.Remove(query.Length - 1, 1);

			

			query.AppendFormat(" WHERE BaseModelOrderId = {1}", type.Name, id);

			return query.ToString();
		}*/
	}
}