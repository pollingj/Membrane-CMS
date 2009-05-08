
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

			Mapper.CreateMap<MembraneUser, UserDetailsResponseDTO>();
			Mapper.CreateMap<UserDetailsRequestDTO, MembraneUser>()
				.ForMember(dest => dest.Group, opt => opt.Ignore())
				.ForMember(dest => dest.Type, opt => opt.Ignore());

			Mapper.CreateMap<UserGroup, UserGroupDTO>();
			Mapper.CreateMap<UserGroupDTO, UserGroup>();

			Mapper.CreateMap<NavigationType, NavigationTypeDTO>();
			Mapper.CreateMap<NavigationTypeDTO, NavigationType>();

			Mapper.CreateMap<NavigationNodeDTO, NavigationNode>()
				.ForMember(dest => dest.Parent, opt => opt.MapFrom(source => source.Parent))
				.ForMember(dest => dest.Parent, opt => opt.FormatNullValueAs(null))
				.ForMember(dest => dest.Published, opt => opt.Ignore())
				.ForMember(dest => dest.ParentEntity_Id, opt => opt.Ignore())
				.ForMember(dest => dest.Culture, opt => opt.Ignore())
				.ForMember(dest => dest.Revision, opt => opt.Ignore());
			Mapper.CreateMap<NavigationNode, NavigationNodeDTO>()
				.ForMember(dest => dest.Parent, opt => opt.MapFrom(source => source.Parent));

		}
	}
}