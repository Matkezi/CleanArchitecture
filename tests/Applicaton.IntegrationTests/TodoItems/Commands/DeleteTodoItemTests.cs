using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using SkipperAgency.Application.Common.Exceptions;
using SkipperAgency.Application.TodoItems.Commands.CreateTodoItem;
using SkipperAgency.Application.TodoItems.Commands.DeleteTodoItem;
using SkipperAgency.Application.TodoLists.Commands.CreateTodoList;
using SkipperAgency.Domain.Entities;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class DeleteTodoItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new DeleteTodoItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteTodoItem()
        {
            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item"
            });

            await SendAsync(new DeleteTodoItemCommand
            {
                Id = itemId
            });

            var list = await FindAsync<TodoItem>(listId);

            list.Should().BeNull();
        }
    }
}
