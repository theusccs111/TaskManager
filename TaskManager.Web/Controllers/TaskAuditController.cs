using Microsoft.AspNetCore.Mvc;
using Task.Manager.Domain.Resource.Request;
using TaskManager.Service.Services;

namespace SOMA.OPEX.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAuditController : ControllerBase
    {
        private readonly TaskAuditService _taskAuditService;

        public TaskAuditController(TaskAuditService taskAuditService)
        {
            _taskAuditService = taskAuditService;
        }

        [HttpPost]
        public IActionResult Add(TaskAuditRequest request)
        {
            var dados = _taskAuditService.CreateWithComplete(request);
            return Ok(dados);
        }


    }
}
