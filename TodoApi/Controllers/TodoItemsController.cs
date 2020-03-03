using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Validators;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;
        public TodoItemsController(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoItemsService.Get();
            return Ok(todoItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemsService.Get(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var hola=  Ok(todoItem);
            return hola;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                _todoItemsService.Update(todoItem);
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
           var itemCreated =  _todoItemsService.Create(todoItem);

            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            //return Ok(itemCreated);
            return Ok();
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {

            var todoItem = await _todoItemsService.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoItemsService.Delete(id);

            return Ok(todoItem);
        }

        bool TodoItemExists(long id)
        {
            return _todoItemsService.TodoItemExists(id);
        }
    }
}
