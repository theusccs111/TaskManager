using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Manager.Domain.Resource.Request;
using TaskManager.Service.Services;

namespace SOMA.OPEX.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IActionResult Add(ProjectRequest request)
        {
            var dados = _projectService.Create(request);
            return Ok(dados);
        }
    }
}
