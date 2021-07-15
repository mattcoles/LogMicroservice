using LogMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly IErrorRepository _repository;

        // Constructor
        public ErrorController(IErrorRepository repository)
        {
            _repository = repository;
        }

        // Get api/log
        [HttpGet]
        public IActionResult Get()
        {
            // Return the result of the processed log.
            return _repository.HandleErrors();
        }
    }
}