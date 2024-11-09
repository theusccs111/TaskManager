using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Manager.Domain.Resource.Request;
using TaskManager.Service.Services;

namespace SOMA.OPEX.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult ListByProject(long projectId)
        {
            TaskGETRequest request = new TaskGETRequest()
            {
                ProjectId = projectId
            };
            var dados = _taskService.ListByProject(request);
            return Ok(dados);
        }

        
    }
}
