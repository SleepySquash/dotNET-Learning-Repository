using AutoMapper;
using WebAPI.Models;

namespace WebAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<UserDTO, Domain.Models.User>();
            this.CreateMap<ConditionDTO, Domain.Models.Condition>();
            this.CreateMap<UserCreateDTO, Domain.Models.User>();
            this.CreateMap<UserUpdateDTO, Domain.Models.User>();
            this.CreateMap<ConditionCreateDTO, Domain.Models.Condition>();
            this.CreateMap<ConditionUpdateDTO, Domain.Models.Condition>();
        }
    }
}