using System.Collections.Generic;
using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.TodoLists.Queries.GetTodos
{
    public class TodoListDto : IMapFrom<TodoList>
{
    public TodoListDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string Title { get; set; }

    public IList<TodoItemDto> Items { get; set; }
}
}
