using KolokwiumAPBD.DTOs;
using KolokwiumAPBD.Service;
using Microsoft.AspNetCore.Mvc;

namespace KolokwiumAPBD.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TasksController : Controller
    {

        private readonly IDBService _dBService;
        public TasksController(IDBService dBService)
        {
            _dBService = dBService;
        }

        [HttpGet("?projectId={projectId}")]
        public async Task<IActionResult> GetTasksAsync([FromRoute]int? projectId)
        {
            return Ok(await _dBService.getTasksAsync(projectId));
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskAsync([FromBody] AddTaskDTO addTaskDTO)
        {
            try
            {
                await _dBService.addTaskAsync(addTaskDTO);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
