using ProducerIrongridC2System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProducerIrongridC2System.Services
{
    public class Loader
    {
        public List<AssetReport> LoadFromJson(string path)
        {
            var data = JsonSerializer.Deserialize<List<AssetReport>>(File.ReadAllText(path));

            //using (JsonDocument document = JsonDocument.Parse(File.ReadAllText(path)))
            //{
            //    foreach (JsonElement doc in document.RootElement.EnumerateArray())
            //    {
            //        data.Add(doc.GetRawText());
            //    }
            //}
            return data;
        }
        public List<AssetReport> CheckType(List<AssetReport> data, string keyWord)
        {
            List<AssetReport> dataSplited = new();
            foreach (AssetReport row in data)
            {
                if (row.AssetType == keyWord)
                {
                    dataSplited.Add(row);
                }
            }
            return dataSplited;
        }
    }
}
