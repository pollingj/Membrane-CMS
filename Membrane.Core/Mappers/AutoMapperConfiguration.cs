using System.Collections.Generic;
using AutoMapper;
using Membrane.Core.DTOs;
using Membrane.Entities;

namespace Membrane.Core.Mappers
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.CreateMap<MembraneUser, AuthenticatedUserDTO>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(source => source.Type.Type))
				.ForMember(dest => dest.Identity, opt => opt.Ignore())
				.ForMember(dest => dest.Name, opt => opt.Ignore())
				.ForMember(dest => dest.AuthenticationType, opt => opt.Ignore())
				.ForMember(dest => dest.IsAuthenticated, opt => opt.Ignore());

			Mapper.CreateMap<UserGroup, UserGroupDTO>();
			Mapper.CreateMap<UserGroupDTO, UserGroup>();
		}
	}
}