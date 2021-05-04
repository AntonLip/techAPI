using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using techAPI.Models.Models;
using techAPI.Models.ModelsDto;

namespace techAPI.Models.Interfaces
{
    public interface IDeviceRepository : IRepository<Device,Guid>
    {
        Task<IEnumerable<Device>> GetFilteredDiveces(DeviceFilter deviceFilter, CancellationToken cancelationToken = default );
    }
}
