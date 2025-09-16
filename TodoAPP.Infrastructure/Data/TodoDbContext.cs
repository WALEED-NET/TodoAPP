using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TodoAPP.Core.Domain_Entity;
using TodoAPP.Infrastructure.Configurations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TodoAPP.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoConfiguration).Assembly); // Folder Of configurations

            base.OnModelCreating(modelBuilder);
        }
    }
}
