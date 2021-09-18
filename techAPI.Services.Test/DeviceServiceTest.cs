using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SampleDataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using techAPI.Models.Interfaces;
using techAPI.Models.SettingsClass;
using techAPI.Models.ModelsDto;
using Xunit;
using techAPI.Models.SettingClass;
using techAPI.Models.Models;

namespace techAPI.Services.Test
{
    public class DeviceServiceTest
    {
        private Mock<IDeviceRepository> _deviceRepository;
        private IDeviceRepository _mockDeviceRepository;
        private DeviceService _deviceService;
        private IMapper _mapper;
        private List<DeviceDto> _fakeDevices;

        private static Random _random;


        public DeviceServiceTest()
        {

        }
        [Fact]
        public async Task<List<DeviceDto>> Device_FindAllDeviceAsync_Return_ListDeviceDTO()
        {
            CreateDefaultDeviceServiceInstance();

            var deviceList = await _deviceService.GetAllAsync(default(CancellationToken));

            Assert.True(Equals(10, _fakeDevices.Count()));
            return (List<DeviceDto>)deviceList;
        }
        [Fact]
        public async Task<DeviceDto> Device_GetByIdAsync_Return_DeviceDto()
        {
            CreateDefaultDeviceServiceInstance();
            var idDevice = _fakeDevices[0].Id;
            var history = await _deviceService.GetByIdAsync(idDevice, default(CancellationToken));

            Assert.NotNull(history);
            return history;
        }
        [Fact]
        public async Task<DeviceDto> Device_RemoveAsync_Return_DeviceDto()
        {
            CreateDefaultDeviceServiceInstance();
            var idDevice = _fakeDevices[0].Id;
            var history = await _deviceService.RemoveAsync(idDevice, default(CancellationToken));

            Assert.NotNull(history);
            return history;
        }
        [Fact]
        public async Task<DeviceDto> Device_UpdateAsync_Return_DeviceDto()
        {
            CreateDefaultDeviceServiceInstance();
            var idDevice = _fakeDevices[0].Id;
            var _fakeDev = _mapper.Map<UpdateDeviceDto>(_fakeDevices[0]);
            var history = await _deviceService.UpdateAsync(idDevice, _fakeDev, default(CancellationToken));

            Assert.NotNull(history);
            return history;
        }
        [Fact]
        public async Task<DeviceDto> Device_AddAsync_Return_DeviceDto()
        {
            CreateDefaultDeviceServiceInstance();
            
            var _fakeDev = _mapper.Map<AddDeviceDto>(_fakeDevices[0]);
            var history = await _deviceService.AddAsync(_fakeDev, default(CancellationToken));

            Assert.NotNull(history);
            return history;
        }

        [Fact]
        public async Task<IEnumerable<DeviceDto>> Device_GetFilteredDevices_Return_ListDeviceDto()
        {
            CreateDefaultDeviceServiceInstance();

            var deviceList = await _deviceService.GetFilteredDevices(new DeviceFilter(), default(CancellationToken));

            Assert.True(Equals(10, _fakeDevices.Count()));
            return deviceList;
        }

        private void CreateDefaultDeviceServiceInstance()
        {
            _random = new Random();
            var bonusGenerator = Generator
                    .For<DeviceDto>()
                    .For(x => x.Id)
                    .ChooseFrom(Guid.NewGuid())
                    .For(x => x.Name)
                    .ChooseFrom(RandomString(10))
                    .For(x => x.Place)
                    .ChooseFrom(RandomString(10))
                    .For(x => x.PreciousMetals)
                    .ChooseFrom(RandomString(10))
                    .For(x => x.SerialNumber)
                    .ChooseFrom(RandomString(10))
                    .For(x => x.Count)
                    .ChooseFrom(_random.Next(1, 10))
                    .For(x => x.AccountingBook)
                    .ChooseFrom(RandomString(10));

            _fakeDevices = bonusGenerator.Generate(10).ToList();

            var services = new ServiceCollection();
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var myProfile = new MapperProfile();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(myProfile);
                cfg.ConstructServicesUsing(serviceProvider.GetService);
            });
            _mapper = new Mapper(configuration);

            _deviceRepository = new Mock<IDeviceRepository>();
            _deviceRepository.Setup(s => s.GetAllAsync(default(CancellationToken))).ReturnsAsync(_mapper.Map<List<Device>>(_fakeDevices));
            _deviceRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Device>(_fakeDevices[1]));
            _deviceRepository.Setup(s => s.RemoveAsync(It.IsAny<Guid>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<Device>(_fakeDevices[1]));
            _deviceRepository.Setup(s => s.AddAsync(It.IsAny<Device>(), default(CancellationToken)));
            _deviceRepository.Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<Device>(), default(CancellationToken)));
            _deviceRepository.Setup(s => s.GetFilteredDiveces(It.IsAny<DeviceFilter>(), default(CancellationToken))).ReturnsAsync(_mapper.Map<List<Device>>(_fakeDevices));
            _mockDeviceRepository = _deviceRepository.Object;
            _deviceService = new DeviceService(_mockDeviceRepository, _mapper);
        }
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }

}
