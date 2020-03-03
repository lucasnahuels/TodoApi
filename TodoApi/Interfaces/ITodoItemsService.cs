using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodoItemsService
    {
        Task Create(TodoItem model);
        Task<TodoItem> Get(long id);
        void Update(TodoItem model);
        Task Delete(long id);
        Task<IEnumerable<TodoItem>> Get();
        bool TodoItemExists(long id);
    }
}
