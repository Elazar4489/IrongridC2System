using IrongridC2SystemAPI.DTOs;
using IrongridC2SystemAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IrongridC2SystemAPI.Controllers
{
    [ApiController]
    [Route("/api/assets-status")]
    public class AssetsStatusController : ControllerBase
    {
        private readonly AssetsStatusRepository _repository;
        public AssetsStatusController(AssetsStatusRepository repository)
        {
            _repository = repository;
        }
        [HttpGet] // 200
        public async Task<ActionResult<AssetsStatusDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("{id}")] // 200, 404, 400
        public async Task<ActionResult<AssetsStatusDto>> GetByID(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("status")]// GET `/api/assets-status?status={status}` = 200
        public async Task<ActionResult<IEnumerable<AssetsStatusDto>>> GetByStatus([FromQuery] string? processedStatus)
        {
            var result = _repository.AssetsStatusesByProcessedStatus(processedStatus??"");
            return Ok(result);
        }

    }
}
