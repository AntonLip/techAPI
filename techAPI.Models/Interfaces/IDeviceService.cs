using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using techAPI.Models.ModelsDto;

namespace techAPI.Models.Interfaces
{
    public interface IDeviceService
    {
        Task<DeviceDto> AddAsync(AddDeviceDto model, CancellationToken cancellationToken = default);
        Task<IEnumerable<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DeviceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<DeviceDto> UpdateAsync(Guid id, UpdateDeviceDto obj, CancellationToken cancellationToken = default);
        Task<DeviceDto> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<DeviceDto>> GetFilteredDevices(DeviceFilter filter, CancellationToken cancellationToken = default);
    }
}
