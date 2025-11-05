using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<User, UserDto>();

        // Todo mappings
        CreateMap<CreateTodoDto, Todo>();
        CreateMap<UpdateTodoDto, Todo>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}