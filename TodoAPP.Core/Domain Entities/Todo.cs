using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoAPP.Core.Domain_Entity
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Priority Priority { get; set; } = Priority.Medium;

        // Domain behavior methods with validations
        
        public void ToggleStatus()
        {
            IsCompleted = !IsCompleted;
            Touch();
        }

        public void MarkCompleted()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                Touch();
            }
        }

      

        public void Update(string title, string description, Priority priority, bool isCompleted)
        {
            
            Title = title.Trim();
            Description = description ?? string.Empty;
            Priority = priority;
            IsCompleted = isCompleted;
            Touch();
        }

        private void Touch()
        {
            if (CreatedAt == default)
            {
                CreatedAt = DateTime.UtcNow;
            }
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
