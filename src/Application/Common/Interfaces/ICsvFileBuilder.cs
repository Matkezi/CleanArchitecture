using System.Collections.Generic;
using SkipperAgency.Application.TodoLists.Queries.ExportTodos;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
