using ConsumerIrongridC2System.Data;
using ConsumerIrongridC2System.Models;
using IrongridC2SystemAPI.DTOs;
using System.Collections.Generic;

namespace IrongridC2SystemAPI.Repositories
{
    public class AssetsStatusRepository
    {
        private readonly ApiDbContext _context;
        public AssetsStatusRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AssetsStatusDto>> GetAllAsync()
        {
            var result = _context.AssetLiveStatuses.Select(a => new AssetsStatusDto
            {
                Id = a.Id,
                AssetId = a.AssetId,
                AssetType = a.AssetType,
                IsVerified = a.IsVerified,
                RawValue = a.RawValue,
                ProcessedStatus = a.ProcessedStatus,
                LastUpdate = a.LastUpdate
            });
            return result;
        }
        public async Task<AssetsStatusDto> GetByIdAsync(int id)
        {
            var theAsset = _context.AssetLiveStatuses.FirstOrDefault(a => a.Id == id);
            if (theAsset == null)
            {
                return null;
            }

            var result = new AssetsStatusDto
            {
                Id = theAsset.Id,
                AssetId = theAsset.AssetId,
                AssetType = theAsset.AssetType,
                IsVerified = theAsset.IsVerified,
                RawValue = theAsset.RawValue,
                ProcessedStatus = theAsset.ProcessedStatus,
                LastUpdate = theAsset.LastUpdate
            };
            return result;
        }
        public async Task<IEnumerable<AssetsStatusDto>> AssetsStatusesByProcessedStatus(string processedStatus)
        {
            var query = _context.AssetLiveStatuses;
            var result = query.Select(a => new AssetsStatusDto
            {
                Id = a.Id,
                AssetId = a.AssetId,
                AssetType = a.AssetType,
                IsVerified = a.IsVerified,
                RawValue = a.RawValue,
                ProcessedStatus = a.ProcessedStatus,
                LastUpdate = a.LastUpdate
            }).Where(a => a.ProcessedStatus == processedStatus);
            return result;
        }
    }
}
