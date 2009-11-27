using System;
using AutoMapper;
using Membrane.Commons.Persistence;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Entities;
using Membrane.Core.Queries.Culture;
using Membrane.Core.Services.Interfaces;

namespace Membrane.Core.Services
{
	public class CultureService : ICultureService
	{
		private readonly IRepository<Culture> repository;

		public CultureService(IRepository<Culture> repository)
		{
			this.repository = repository;
		}

		public CultureDTO GetDefaultCulture()
		{
			var defaultCulture = repository.FindOne(new DefaultCulture());

			return Mapper.Map<Culture, CultureDTO>(defaultCulture);
		}
	}
}