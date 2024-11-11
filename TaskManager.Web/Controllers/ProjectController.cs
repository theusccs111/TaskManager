using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Manager.Domain.Resource.Request;
using TaskManager.Service.Services;

namespace TaskManager.Web.Controllers
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

        [HttpGet]
        public IActionResult ListByUser(long userId)
        {
            ProjectGETRequest request = new ProjectGETRequest()
            {
                UserId = userId
            };
            var dados = _projectService.ListByUser(request);
            return Ok(dados);
        }

        [HttpPost]
        public IActionResult Add(ProjectRequest request)
        {
            var dados = _projectService.Create(request);
            return Ok(dados);
        }


        [HttpPut]
        public IActionResult Update(ProjectRequest request)
        {
            var dados = _projectService.Update(request);
            return Ok(dados);
        }

        [HttpDelete]
        public IActionResult Delete(ProjectRequest request)
        {
            var dados = _projectService.Delete(request);
            return Ok(dados);
        }
    }
}
