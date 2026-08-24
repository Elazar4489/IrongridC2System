using ConsumerIrongridC2System.Data;
using ConsumerIrongridC2System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumerIrongridC2System.Services
{
    public class ConsumerService
    {
        private readonly ConsumerDbContext _context;
        public ConsumerService(ConsumerDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ConsunTypeUAV(string jsonMessage)
        {
            AssetReport? asset = JsonSerializer.Deserialize<AssetReport>(jsonMessage);
            if (asset == null)
            {
                return false;
            }
            AssetLiveStatus assetLiveStatus = ValidateAndCalculateUAV(asset);
            var theLastAsset = await _context.AssetLiveStatuses.FirstOrDefaultAsync(a => a.AssetId == assetLiveStatus.AssetId);
            if (theLastAsset == null)
            {
                var dd = await _context.AssetLiveStatuses.AddAsync(assetLiveStatus);
                Console.WriteLine("added");
            }
            else
            {
                await _context.AssetLiveStatuses
                .Where(a => a.Id == theLastAsset.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(a=>a.AssetId, assetLiveStatus.AssetId)
                .SetProperty(a => a.AssetType, assetLiveStatus.AssetType)
                .SetProperty(a => a.RawValue, assetLiveStatus.RawValue)
                .SetProperty(a => a.ProcessedStatus, assetLiveStatus.ProcessedStatus)
                .SetProperty(a => a.IsVerified, assetLiveStatus.IsVerified)
                .SetProperty(a => a.LastUpdate, assetLiveStatus.LastUpdate));
                Console.WriteLine("updated");
            }
            await _context.SaveChangesAsync();
            
            return true;

        }
        public AssetLiveStatus ValidateAndCalculateUAV(AssetReport asset)
        {
            string processedStatus = "";
            bool isVerified = false;
            if (int.TryParse(asset.RawValue, out int result))
            {
                if (result >= 20 && result <= 100)
                {
                    processedStatus = "Stable";
                    isVerified = true;
                }
                else if(result >= 0)
                {
                    processedStatus = "Warning";
                    isVerified = true;
                }
                else
                {
                    processedStatus = "Warning";
                    isVerified = false;
                }
            }
            
            var assetLiveStatus = new AssetLiveStatus
            {
                AssetId = asset.AssetId,
                AssetType = asset.AssetType,
                RawValue = asset.RawValue,
                ProcessedStatus = processedStatus,
                IsVerified = isVerified,
                LastUpdate = asset.Timestamp
            };
            return assetLiveStatus;
        }
        public async Task<bool> ConsunTypePerimeterSensors(string jsonMessage)
        {
            AssetReport? asset = JsonSerializer.Deserialize<AssetReport>(jsonMessage);
            if (asset == null)
            {
                return false;
            }
            AssetLiveStatus assetLiveStatus = ValidateAndCalculatePerimeterSensors(asset);
            var theLastAsset = await _context.AssetLiveStatuses.FirstOrDefaultAsync(a => a.AssetId == assetLiveStatus.AssetId);
            if (theLastAsset == null)
            {
                await _context.AssetLiveStatuses.AddAsync(assetLiveStatus);
                Console.WriteLine("added");
            }
            else
            {
                await _context.AssetLiveStatuses
                .Where(a => a.Id == theLastAsset.Id)
                .ExecuteUpdateAsync(setters => setters
                .SetProperty(a => a.AssetId, assetLiveStatus.AssetId)
                .SetProperty(a => a.AssetType, assetLiveStatus.AssetType)
                .SetProperty(a => a.RawValue, assetLiveStatus.RawValue)
                .SetProperty(a => a.ProcessedStatus, assetLiveStatus.ProcessedStatus)
                .SetProperty(a => a.IsVerified, assetLiveStatus.IsVerified)
                .SetProperty(a => a.LastUpdate, assetLiveStatus.LastUpdate));
                Console.WriteLine("updeted");
            }
            await _context.SaveChangesAsync();
            return true;

        }
        public AssetLiveStatus ValidateAndCalculatePerimeterSensors(AssetReport asset)
        {
            string processedStatus = "";
            bool isVerified = false;
            if (asset.RawValue == "Good" || asset.RawValue == "GOOD" || asset.RawValue =="good" || asset.RawValue == "gud")
            {
                processedStatus = "Stable";
                isVerified = true;
            }
            else if (asset.RawValue == "Bad" || asset.RawValue == "BAD" || asset.RawValue == "bad" || asset.RawValue == "bed")
            {
                processedStatus = "Warning";
                isVerified = true;
            }
            else
            {
                processedStatus = "Warning";
                isVerified = false;
            }

            var assetLiveStatus = new AssetLiveStatus
            {
                AssetId = asset.AssetId,
                AssetType = asset.AssetType,
                RawValue = asset.RawValue,
                ProcessedStatus = processedStatus,
                IsVerified = isVerified,
                LastUpdate = asset.Timestamp
            };
            return assetLiveStatus;
        }
    }
}
