using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoBasicAPI.Data;
using TodoBasicAPI.Models;
using TodoBasicAPI.ViewModels;

namespace TodoBasicAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context.Todos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(todo);
        }

        [HttpPost]
        [Route("todos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel createTodoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var todo = new Todo
            {
                Title = createTodoViewModel.Title,
                Done = false,
                Date = DateTime.Now
            };

            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created($"v1/todos/{todo.Id}", todo);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] UpdateTodoViewModel updateTodoViewModel, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var todo = await context.Todos.FirstOrDefaultAsync(filter => filter.Id == id);
                
                todo.Title = updateTodoViewModel.Title;
                todo.Done = updateTodoViewModel.Done;

                await context.SaveChangesAsync();
                return Ok(todo);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("todos/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();
                return Ok("Removido com sucesso!");
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}