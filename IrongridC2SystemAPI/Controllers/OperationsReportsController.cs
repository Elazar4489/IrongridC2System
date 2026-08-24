using IrongridC2SystemAPI.DTOs;
using IrongridC2SystemAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IrongridC2SystemAPI.Controllers
{
    [ApiController]
    [Route("/api/reports")]
    public class OperationsReportsController : ControllerBase
    {
        private readonly OperationReportsRepository _repository;
        public OperationsReportsController(OperationReportsRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("/critical-assets")] // 200
        public async Task<ActionResult<IEnumerable<CriticalAssetsDto>>> GetCriticalAssets()
        {
            var result = await _repository.CriticalAssetsDtos();
            return Ok(result);
        }
        [HttpGet("/unit/{unitId}/assets")] //200, 404
        public async Task<ActionResult<IEnumerable<ReportUnitDto>>> GetReportByUnitId(int unitId)
        {
            var result = await _repository.ReportUnitDtos(unitId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("/summary-by-unit")] // 200
        public async Task<ActionResult<SummaryByUnitDto>> GetSummaryOfUnit()
        {
            var result = _repository.GetSummaryOfUnitAsync();
            return Ok(result);
        }
    }
}
