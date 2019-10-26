using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<TodosController> _logger;
        private readonly ITodosDbService _todosService;

        public TodosController(ILogger<TodosController> logger, ITodosDbService todosService)
        {
            _logger = logger;
            this._todosService = todosService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _todosService.Load();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Todo todo)
        {
            _todosService.Add(todo);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Remove([FromBody] Todo todo)
        {
            _todosService.Remove(todo);
            return Ok();
        }
    }
}
