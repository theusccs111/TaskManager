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

        [HttpGet("ReportPerformance")]
        public IActionResult ReportPerformance(long userId)
        {
            ReportRequest request = new ReportRequest()
            {
                UserId = userId
            };
            var dados = _taskService.ReportPerformance(request);
            return Ok(dados);
        }

        [HttpPost]
        public IActionResult Add(TaskRequest request)
        {
            var dados = _taskService.Create(request);
            return Ok(dados);
        }

        [HttpPut]
        public IActionResult Update(TaskRequest request)
        {
            var dados = _taskService.Update(request);
            return Ok(dados);
        }

        [HttpDelete]
        public IActionResult Delete(TaskRequest request)
        {
            var dados = _taskService.Delete(request);
            return Ok(dados);
        }


    }
}
