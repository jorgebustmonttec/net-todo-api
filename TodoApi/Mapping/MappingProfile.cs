using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<User, UserDto>();

        // keep existing Todo mappings if any; if not, controllers can rely on current DTOs
        // CreateMap<CreateTodoDto, Todo>();
        // CreateMap<UpdateTodoDto, Todo>();
        // CreateMap<Todo, TodoDto>();
    }
}