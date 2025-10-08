using AutoMapper;
using TodoApi.Models;

namespace TodoApi.Profiles;

public class TodosProfile : Profile
{
    public TodosProfile()
    {
        // Source - > Target
        CreateMap<CreateTodoDto, Todo>();
        CreateMap<updateTodoDto, Todo>();
    }
}