using LogMicroservice.Repositories.Entities;
using LogMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _repository;

        // Constructor
        public LogController(ILogRepository repository)
        {
            _repository = repository;
        }

        // POST api/log
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Log log)
        {
            // Return the result of the processed log.
            return await _repository.ProcessLog(log);
        }
    }
}
