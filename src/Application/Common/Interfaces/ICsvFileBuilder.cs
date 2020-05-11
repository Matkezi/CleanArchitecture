using SkipperAgency.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
