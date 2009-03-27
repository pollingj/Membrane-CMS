using AutoMapper;
using Membrane.Core.DTOs;
using Membrane.Entities;
using NUnit.Framework;

namespace Membrane.Tests.Unit.Core.Mappers
{
	[TestFixture]
	public class MembraneUserToAuthenticatedUserDTOMapperFixture
	{
		[Test]
		public void CanSuccessFullyMap()
		{
			Mapper.CreateMap<MembraneUser, AuthenticatedUserDTO>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.Type))
				.ForMember(dest => dest.Identity, opt => opt.Ignore())
				.ForMember(dest => dest.Name, opt => opt.Ignore())
				.ForMember(dest => dest.AuthenticationType, opt => opt.Ignore())
				.ForMember(dest => dest.IsAuthenticated, opt => opt.Ignore());
			Mapper.AssertConfigurationIsValid();
		}
	}
}