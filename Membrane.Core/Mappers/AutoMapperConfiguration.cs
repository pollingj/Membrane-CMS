
using AutoMapper;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Entities;
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
			Mapper.CreateMap<MembraneUser, UserDetailsRequestDTO>()
				.ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());

			Mapper.CreateMap<UserGroup, UserGroupDTO>();
			Mapper.CreateMap<UserGroupDTO, UserGroup>();

			Mapper.CreateMap<NavigationType, NavigationTypeDTO>();
			Mapper.CreateMap<NavigationTypeDTO, NavigationType>();

			Mapper.CreateMap<Culture, CultureDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Language));
			Mapper.CreateMap<CultureDTO, Culture>()
				.ForMember(dest => dest.Language, opt => opt.MapFrom(source => source.Name))
				.ForMember(dest => dest.ShortCode, opt => opt.Ignore())
				.ForMember(dest => dest.IsDefault, opt => opt.Ignore());

			Mapper.CreateMap<NavigationNodeDTO, NavigationNode>()
				.ForMember(dest => dest.Parent, opt => opt.MapFrom(source => source.Parent))
				.ForMember(dest => dest.Parent, opt => opt.FormatNullValueAs(null))
				.ForMember(dest => dest.Published, opt => opt.Ignore())
				.ForMember(dest => dest.ParentEntity_Id, opt => opt.Ignore())
				.ForMember(dest => dest.Revision, opt => opt.Ignore());
			Mapper.CreateMap<NavigationNode, NavigationNodeDTO>()
				.ForMember(dest => dest.Parent, opt => opt.MapFrom(source => source.Parent));

			Mapper.CreateMap<InstalledPlugin, InstalledPluginDTO>();
		}
	}
}