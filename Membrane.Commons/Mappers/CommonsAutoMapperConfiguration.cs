
using AutoMapper;
using Membrane.Commons.Plugin.DTOs;
using Membrane.Commons.Plugin.Entities;

namespace Membrane.Commons.Mappers
{
	public class CommonsAutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.CreateMap<Culture, CultureDTO>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(source => source.Language));
			Mapper.CreateMap<CultureDTO, Culture>()
				.ForMember(dest => dest.Language, opt => opt.MapFrom(source => source.Name))
				.ForMember(dest => dest.ShortCode, opt => opt.Ignore())
				.ForMember(dest => dest.IsDefault, opt => opt.Ignore());

		}
	}
}