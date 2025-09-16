using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLY.Portal.Application.Common.Models;
using TodoAPP.Core.Dtos;

namespace TodoAPP.Core.Interfaces
{
    
    public interface ITodoService
    {
    Task<ApiResponse<TodoResponseDto>> CreateAsync(CreateTodoDto dto);
    Task<ApiResponse<TodoResponseDto?>> GetByIdAsync(int id);
    Task<ApiResponse<IEnumerable<TodoResponseDto>>> GetAllAsync();
    Task<ApiResponse<TodoResponseDto?>> UpdateAsync(int id, UpdateTodoDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
    Task<ApiResponse<TodoResponseDto?>> ToggleStatusAsync(int id);
    }
}
