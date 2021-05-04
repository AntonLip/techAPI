using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using techAPI.Models.Interfaces;
using techAPI.Models.Models;
using techAPI.Models.ModelsDto;

namespace techAPI.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        public DeviceService(IDeviceRepository deviceRepository,
                             IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
        }
        public async Task<DeviceDto> AddAsync(AddDeviceDto model, CancellationToken cancellationToken = default)
        {
           if(model is null)
            {
                throw new ArgumentNullException();
            }
            var device = _mapper.Map<Device>(model);
            await _deviceRepository.AddAsync(device);

            return _mapper.Map<DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var devices =  await _deviceRepository.GetAllAsync(cancellationToken);
            return devices is null ? throw new ArgumentNullException() : _mapper.Map<List<DeviceDto>>(devices);
        }

        public async Task<DeviceDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            var device = await _deviceRepository.GetByIdAsync(id, cancellationToken);
            return device is null ? throw new ArgumentNullException() : _mapper.Map<DeviceDto>(device);
        }

        public async Task<IEnumerable<DeviceDto>> GetFilteredDevices(DeviceFilter filter, CancellationToken cancellationToken = default)
        {
            if (filter is null)
            {
                throw new ArgumentNullException();
            }
            var device = await _deviceRepository.GetFilteredDiveces(filter, cancellationToken);
            return device is null ? throw new ArgumentNullException() : _mapper.Map<List<DeviceDto>>(device);
        }       

        public async Task<DeviceDto> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var device = await _deviceRepository.GetByIdAsync(id, cancellationToken);
            if (device is null)
            {
                throw new ArgumentNullException();
            }
             _deviceRepository.RemoveAsync(id);
            return _mapper.Map<DeviceDto>(device);
        }

        public async Task<DeviceDto> UpdateAsync(Guid id, UpdateDeviceDto model, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty || model is null || id != model.Id)
            {
                throw new ArgumentNullException();
            }
            var newDevice = _mapper.Map<Device>(model);
            await _deviceRepository.UpdateAsync(id, newDevice, cancellationToken);
            return _mapper.Map<DeviceDto>(newDevice);
        }
    }
}
