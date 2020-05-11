using SkipperAgency.Application.Common.Mappings;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
