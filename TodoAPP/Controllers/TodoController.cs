using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoAPP.Core.Dtos;
using TodoAPP.Core.Interfaces;
using SLY.Portal.Application.Common.Models;

namespace TodoAPP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TodoResponseDto>>> Create([FromBody] CreateTodoDto dto)
        {
            var result = await _todoService.CreateAsync(dto);
            return StatusCode(result.Success ? 201 : 400, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TodoResponseDto?>>> GetById(int id)
        {
            var result = await _todoService.GetByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TodoResponseDto>>>> GetAll()
        {
            var result = await _todoService.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TodoResponseDto?>>> Update(int id, [FromBody] UpdateTodoDto dto)
        {
            var result = await _todoService.UpdateAsync(id, dto);
            if (!result.Success && result.Error?.Code == "NOT_FOUND")
                return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var result = await _todoService.DeleteAsync(id);
            if (!result.Success && result.Error?.Code == "NOT_FOUND")
                return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("{id}/toggle-status")]
        public async Task<ActionResult<ApiResponse<TodoResponseDto?>>> ToggleStatus(int id)
        {
            var result = await _todoService.ToggleStatusAsync(id);
            if (!result.Success && result.Error?.Code == "NOT_FOUND")
                return NotFound(result);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
