using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreJourney.API.Controllers
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

        [HttpGet]
        public async Task<ActionResult<List<Log>>> GetAllStaticLogs()
        {
            var logs= await _logStaticRepository.GetAllStaticLogsAsync();
            return Ok(logs);
        }
        [HttpGet("static-log")]
        public async Task<ActionResult<Log?>> GetStaticLogById()
        {
             var logs = await _logStaticRepository.GetStaticLogByIdAsync();
            if (logs==null)
                  return NotFound("Static ID ile kayıt bulunamadı.");// staticID = 4
            
            return Ok(logs);
        }
        [HttpPost("add-static-log")]
        public async Task<IActionResult> AddStaticLog()
        {
            try
            {
                await _logStaticRepository.AddStaticLogAsync();
                return Ok("Static Log Başarıyla Eklendi");
            }
            catch (Exception ex)
            {

                return BadRequest($"Bir hata oluştu: {ex.Message}");
            }
        }
        [HttpPut("update-static-log")]
        public async Task<IActionResult> UpdateStaticLog()
        {
            await _logStaticRepository.UpdateStaticLogAsync();
            try
            {
                return Ok("Static Log Başarıyla Güncellendi");
            }
            catch (Exception ex)
            {

                return BadRequest($"Bir hata oluştu: {ex.Message}");
            }
        }
        [HttpDelete("delete-static-log")]
        public async Task<IActionResult> DeleteStaticLog()
        {
            await _logStaticRepository.DeleteStaticLogAsync();  
            try
            {
                return Ok("Static Log Başarıyla Silindi");
            }
            catch (Exception ex)
            {

                return BadRequest($"Bir hata oluştu: {ex.Message}");
            }
        }
    }
}
