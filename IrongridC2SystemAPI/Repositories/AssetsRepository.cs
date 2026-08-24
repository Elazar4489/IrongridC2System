using ConsumerIrongridC2System.Data;
using ConsumerIrongridC2System.Models;
using IrongridC2SystemAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace IrongridC2SystemAPI.Repositories
{
    public class AssetsRepository
    {
        private readonly ApiDbContext _context;
        public AssetsRepository(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<Asset?> GetByIdAsync(int id)
        {
            var result = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);
            if (result == null)
            {
                return null;
            }
            return result;
        }

        public async Task<bool> CreateUnitAsync(CreateUnitDto newUnit)
        {
            var unit = new Unit
            {
                Id = newUnit.Id,
                Sector = newUnit.Sector,
                UnitName = newUnit.UnitName
            };
            await _context.Units.AddAsync(unit);
            await _context.SaveChangesAsync();
            return true;
        } 
        public async Task<bool> UpdateAssetAsync(UpdateAssetDto asset, int id)
        {
            var theAsset = await GetByIdAsync(id);
            if (theAsset == null)
            {
                return false;
            }
            await _context.Assets.Where(a => a.Id == id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(a => a.UnitId, asset.UnitId)
                .SetProperty(a => a.AssetSerial, asset.AssetSerial)
                .SetProperty(a => a.Type, asset.Type)
                );
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAssetAsync(int id)
        {
            var theAsset = await GetByIdAsync(id);
            if (theAsset == null)
            {
                return false;
            }
            _context.Assets.Remove(theAsset);
            return true;
        }
    }
}
