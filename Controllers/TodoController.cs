using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoBasicAPI.Data;
using TodoBasicAPI.Models;

namespace TodoBasicAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public List<Todo> Get([FromServices] AppDbContext context)
        {
            var todos = context.Todos.ToList();
            return todos;
        }
    }
}