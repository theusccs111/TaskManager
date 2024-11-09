using Microsoft.AspNetCore.Mvc;
using Task.Manager.Domain.Resource.Request;
using TaskManager.Service.Interface.Services;
using TaskManager.Service.Services;

namespace SOMA.OPEX.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAuditController : ControllerBase
    {
        private readonly ITaskAuditService _taskAuditService;

        public TaskAuditController(ITaskAuditService taskAuditService)
        {
            _taskAuditService = taskAuditService;
        }

        [HttpGet]
        public IActionResult ListByTask(long taskId)
        {
            var dados = _taskAuditService.List(taskId);
            return Ok(dados);
        }

        [HttpPost]
        public IActionResult Add(TaskAuditRequest request)
        {
            var dados = _taskAuditService.CreateWithComplete(request);
            return Ok(dados);
        }


    }
}
