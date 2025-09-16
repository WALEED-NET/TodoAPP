using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAPP.Core.Domain_Entity;

namespace TodoAPP.Core.Dtos
{
    public record CreateTodoDto(
    string Title,
    string Description,
    Priority Priority = Priority.Medium)
    {
        public static implicit operator Todo(CreateTodoDto dto) => new()
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
    }


    public record UpdateTodoDto(string Title, string Description, Priority Priority, bool IsCompleted);


    public class TodoResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Implicit conversion from Entity to DTO
        public static implicit operator TodoResponseDto?(Todo? entity)
        {
            if (entity == null) return null;

            return new TodoResponseDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted,
                Priority = entity.Priority,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
