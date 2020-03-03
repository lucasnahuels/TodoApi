using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        public TodoContext() { }

        public virtual DbSet<TodoItem> TodoItems { get; set; } //virtual?? para que pueda sobreescribirse en el test
    }
}