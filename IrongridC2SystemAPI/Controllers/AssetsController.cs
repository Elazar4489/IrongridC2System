using ConsumerIrongridC2System.Models;
using IrongridC2SystemAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using IrongridC2SystemAPI.DTOs;

namespace IrongridC2SystemAPI.Controllers
{
    [ApiController]
    [Route("/api/assets")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetsRepository _repository;
        public AssetsController(AssetsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("/{id}")] // 200, 404, 400
        public async Task<ActionResult<Asset>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("/units")] // 201, 400
        public async Task<IActionResult> CreateUnit(CreateUnitDto unit)
        {
            var result = await _repository.CreateUnitAsync(unit);
            return StatusCode(201);
        }
        [HttpPut("{id}")] // 200, 404, 400
        public async Task<IActionResult> UpdateAsset(UpdateAssetDto asset, int id)
        {
            var result = await _repository.UpdateAssetAsync(asset, id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")] //204, 404
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var result = await _repository.DeleteAssetAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
