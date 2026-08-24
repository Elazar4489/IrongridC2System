using ConsumerIrongridC2System.Data;
using IrongridC2SystemAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IrongridC2SystemAPI.Repositories
{
    public class OperationReportsRepository
    {
        private readonly ApiDbContext _context;
        public OperationReportsRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CriticalAssetsDto>> CriticalAssetsDtos()
        {
            var query = _context.AssetLiveStatuses;
            var result = query.Select(a => new CriticalAssetsDto
            {
                AssetId = a.AssetId,
                AssetSerial = a.Asset.AssetSerial,
                AssetType = a.AssetType,
                Sector = a.Asset.Unit.Sector,
                UnitName = a.Asset.Unit.UnitName,
                IsVerified = a.IsVerified,
                ProcessedStatus = a.ProcessedStatus,
                LastUpdate = a.LastUpdate
            })
                .Where(a => a.IsVerified == false || a.ProcessedStatus == "Warning");
            return result;
        }
        public async Task<IEnumerable<ReportUnitDto>> ReportUnitDtos(int unitId)
        {
            var theUnit = _context.Units.FirstOrDefault(a => a.Id == unitId);
            if (theUnit == null)
            {
                return null;
            }
            var query = _context.AssetLiveStatuses;
            var result = query.Where(a => a.Asset.UnitId == unitId).Select(a => new ReportUnitDto
            {
                AssetId = a.AssetId,
                AssetSerial = a.Asset.AssetSerial,
                AssetType = a.AssetType,
                IsVerified = a.IsVerified,
                ProcessedStatus = a.ProcessedStatus,
                LastUpdate = a.LastUpdate
            });
            return result;
        }
        public async Task<IEnumerable<SummaryByUnitDto>> GetSummaryOfUnitAsync()
        {
            var query = _context.Units.ToList();
            var result = query.Select(u=>new SummaryByUnitDto
            {
                UnitId = u.Id,
                UnitName = u.UnitName,
                Sector = u.Sector,
                TotalAssets = _context.Assets.Count(a=>a.UnitId == u.Id),
                StableAssets = _context.AssetLiveStatuses.Count(a=>a.Asset.UnitId == u.Id && a.ProcessedStatus == "Stable"),
                WarningAssets = _context.AssetLiveStatuses.Count(a => a.Asset.UnitId == u.Id && a.ProcessedStatus == "Warning"),
                UnverifiedAssets = _context.AssetLiveStatuses.Count(a => a.Asset.UnitId == u.Id && a.IsVerified == false)
            });
            return result;
        }
    }
}
