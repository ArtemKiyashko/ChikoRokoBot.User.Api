using AutoMapper;
using ChikoRokoBot.User.Api.Entities;
using ChikoRokoBot.User.Api.Models;

namespace ChikoRokoBot.User.Api.Mappers
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
	}
}

