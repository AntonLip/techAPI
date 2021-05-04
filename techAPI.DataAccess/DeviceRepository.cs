using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using techAPI.Models.Interfaces;
using techAPI.Models.Models;
using techAPI.Models.ModelsDto;
using techAPI.Models.SettingsClass;

namespace techAPI.DataAccess
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(IOptions<MongoDBSettings> mongoDbSettings)
            : base(mongoDbSettings)
        {

        }

        public async Task<IEnumerable<Device>> GetFilteredDiveces(DeviceFilter deviceFilter, CancellationToken cancelationToken = default)
        {
            FilterDefinition<Device> filter =
                   Builders<Device>.Filter.Eq((x => x.IsDeleted), false);

            if (deviceFilter?.Name is null)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.Name), deviceFilter.Name);
            }

            if (deviceFilter?.AccountingBook is null)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.AccountingBook), deviceFilter.AccountingBook);
            }
            if (deviceFilter?.Place is null)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.Place), deviceFilter.Place);
            }
            if (deviceFilter?.PreciousMetals is null)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.PreciousMetals), deviceFilter.PreciousMetals);
            }
            if (deviceFilter?.SerialNumber is null)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.SerialNumber), deviceFilter.SerialNumber);
            }
            if (deviceFilter?.Category > 0 && deviceFilter?.Category < 5)
            {
                filter = filter & Builders<Device>.Filter.Eq((x => x.Category), deviceFilter.Category);
            }
            return await GetCollection().Find(filter).ToListAsync(cancelationToken);
        }
    }
}
