using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Validators;

namespace TodoApi.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly TodoContext _context;
        private readonly TodoItemValidator _todoItemValidator;
        public TodoItemsService(TodoContext context, TodoItemValidator todoItemValidator)
        {
            _context = context;
            _todoItemValidator = todoItemValidator;
        }

        public async Task Create(TodoItem model)
        {
            _todoItemValidator.ValidateAndThrow(model);
            _context.TodoItems.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var todoItem = await Get(id); 
            _todoItemValidator.ValidateAndThrow(todoItem);
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> Get(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> Get()
        {
            var todoItem = await _context.TodoItems.ToListAsync();
            return todoItem;
        }

        public void Update(TodoItem todoItem)
        {
            _todoItemValidator.ValidateAndThrow(todoItem);

            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public bool TodoItemExists(long id)
        {
            bool result = _context.TodoItems.Any(e => e.Id == id);
            return result;
        }
    }
}
