using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreJourney.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogStaticController : ControllerBase
    {
        private readonly ILogStaticRepository _logStaticRepository;

        public LogStaticController(ILogStaticRepository logStaticRepository)
        {
            _logStaticRepository = logStaticRepository;
        }

        // GET: api/LogStatic
        [HttpGet]
        public async Task <ActionResult<List<Log>>> GetAllStaticLogs()
        {
            var logs= await _logStaticRepository.GetAllStaticLogsAsync();
            return Ok(logs);
        }

        // GET: api/logs/static
        [HttpGet("static")]
        public async Task<ActionResult<Log?>> GetStaticLogById()
        {
            var logs= await _logStaticRepository.GetStaticLogByIdAsync();
            if(logs == null)
                return NotFound();
            
            return Ok(logs);
        }
    }
}
