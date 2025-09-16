using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoAPP.Core.Domain_Entity;
using TodoAPP.Core.Dtos;
using TodoAPP.Core.Interfaces;
using TodoAPP.Infrastructure.Data;
using SLY.Portal.Application.Common.Models;

namespace TodoAPP.Infrastructure.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }


        public async Task<ApiResponse<TodoResponseDto>> CreateAsync(CreateTodoDto dto)
        {
            var validation = ValidateCreate(dto);
            if (!validation.Success)
                return new ApiResponse<TodoResponseDto>(validation.Error!.Code, validation.Error.Message);
            try
            {
                Todo entity = dto;
                await _context.Todos.AddAsync(entity);
                await _context.SaveChangesAsync();
                var response = new TodoResponseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    IsCompleted = entity.IsCompleted,
                    Priority = entity.Priority,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                return new ApiResponse<TodoResponseDto>(response, "Todo created successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoResponseDto>("SERVER_ERROR", ex.Message);
            }
        }

        public async Task<ApiResponse<TodoResponseDto?>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
                if (entity is null)
                    return new ApiResponse<TodoResponseDto?>("NOT_FOUND", $"Todo with id {id} not found.");
                var response = new TodoResponseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    IsCompleted = entity.IsCompleted,
                    Priority = entity.Priority,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                return new ApiResponse<TodoResponseDto?>(response);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoResponseDto?>("SERVER_ERROR", ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<TodoResponseDto>>> GetAllAsync()
        {
            try
            {
                var list = await _context.Todos
                    .AsNoTracking()
                    .OrderByDescending(t => t.CreatedAt)
                    .Select(entity => new TodoResponseDto
                    {
                        Id = entity.Id,
                        Title = entity.Title,
                        Description = entity.Description,
                        IsCompleted = entity.IsCompleted,
                        Priority = entity.Priority,
                        CreatedAt = entity.CreatedAt,
                        UpdatedAt = entity.UpdatedAt
                    })
                    .ToListAsync();
                return new ApiResponse<IEnumerable<TodoResponseDto>>(list);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<TodoResponseDto>>("SERVER_ERROR", ex.Message);
            }
        }

        public async Task<ApiResponse<TodoResponseDto?>> UpdateAsync(int id, UpdateTodoDto dto)
        {
            var validation = ValidateUpdate(dto);
            if (!validation.Success)
                return new ApiResponse<TodoResponseDto?>(validation.Error!.Code, validation.Error.Message);
            try
            {
                var entity = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
                if (entity is null)
                    return new ApiResponse<TodoResponseDto?>("NOT_FOUND", $"Todo with id {id} not found.");
                entity.Update(dto.Title, dto.Description, dto.Priority, dto.IsCompleted);
                await _context.SaveChangesAsync();
                var response = new TodoResponseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    IsCompleted = entity.IsCompleted,
                    Priority = entity.Priority,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                return new ApiResponse<TodoResponseDto?>(response, "Todo updated successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoResponseDto?>("SERVER_ERROR", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
                if (entity is null)
                    return new ApiResponse<bool>("NOT_FOUND", $"Todo with id {id} not found.");
                _context.Todos.Remove(entity);
                await _context.SaveChangesAsync();
                return new ApiResponse<bool>(true, "Todo deleted successfully.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>("SERVER_ERROR", ex.Message);
            }
        }

        public async Task<ApiResponse<TodoResponseDto?>> ToggleStatusAsync(int id)
        {
            try
            {
                var entity = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
                if (entity is null)
                    return new ApiResponse<TodoResponseDto?>("NOT_FOUND", $"Todo with id {id} not found.");

                entity.ToggleStatus();
                await _context.SaveChangesAsync();
                var response = new TodoResponseDto
                {
                    Id = entity.Id,
                    Title = entity.Title,
                    Description = entity.Description,
                    IsCompleted = entity.IsCompleted,
                    Priority = entity.Priority,
                    CreatedAt = entity.CreatedAt,
                    UpdatedAt = entity.UpdatedAt
                };
                return new ApiResponse<TodoResponseDto?>(response, "Todo status toggled.");
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoResponseDto?>("SERVER_ERROR", ex.Message);
            }
        }

         // Validation rules
        private static ApiResponse<bool> ValidateCreate(CreateTodoDto dto)
        {
            if (dto is null)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Request body is required.");
            if (string.IsNullOrWhiteSpace(dto.Title))
                return new ApiResponse<bool>("VALIDATION_ERROR", "Title is required.");
            if (dto.Title.Length < 3 || dto.Title.Length > 200)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Title must be between 3 and 200 characters.");
            if (!string.IsNullOrEmpty(dto.Description) && dto.Description.Length > 2000)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Description cannot exceed 2000 characters.");
            return new ApiResponse<bool>(true);
        }

        private static ApiResponse<bool> ValidateUpdate(UpdateTodoDto dto)
        {
            if (dto is null)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Request body is required.");

            if (string.IsNullOrWhiteSpace(dto.Title))
                return new ApiResponse<bool>("VALIDATION_ERROR", "Title is required.");

            if (dto.Title.Length < 3 || dto.Title.Length > 200)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Title must be between 3 and 200 characters.");
                
            if (!string.IsNullOrEmpty(dto.Description) && dto.Description.Length > 2000)
                return new ApiResponse<bool>("VALIDATION_ERROR", "Description cannot exceed 2000 characters.");

            return new ApiResponse<bool>(true);
        }

    }
}
