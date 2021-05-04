using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using techApi.Models.DTOModels;
using techAPI.Filters;
using techAPI.Models.Interfaces;
using techAPI.Models.ModelsDto;

namespace techAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionFilter]
    [HttpModelResultFilter]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<ActionResult<ResultDto<IEnumerable<DeviceDto>>>> GetAllDevicesAsync()
        {
            return Ok(await _deviceService.GetAllAsync());
        }

        [HttpGet]
        [Route("{deviceId:Guid}")]
        public async Task<ActionResult<ResultDto<IEnumerable<DeviceDto>>>> GetDeviceByIdAsync([FromRoute] Guid deviceId)
        {
            return Ok(await _deviceService.GetByIdAsync(deviceId));
        }

        [HttpPut]
        [Route("{deviceId:Guid}")]
        public async Task<ActionResult<ResultDto<IEnumerable<DeviceDto>>>> UpdateDeviceByIdAsync([FromRoute] Guid deviceId, [FromBody] UpdateDeviceDto model)
        {
            return Ok(await _deviceService.UpdateAsync(deviceId, model));
        }

        [HttpDelete]
        [Route("{deviceId:Guid}")]
        public async Task<ActionResult<ResultDto<IEnumerable<DeviceDto>>>> RemoveDeviceByIdAsync([FromRoute] Guid deviceId)
        {
            return Ok(await _deviceService.RemoveAsync(deviceId));
        }

        [HttpPost]
        public async Task<ActionResult<ResultDto<IEnumerable<DeviceDto>>>> CreateDeviceByIdAsync([FromBody] AddDeviceDto model)
        {
            return Ok(await _deviceService.AddAsync(model));
        }
        [HttpPost]
        [Route("filtered")]
        public async Task<ActionResult<ResultDto<DeviceDto>>> GetFilteredCadetsAsync([FromBody] DeviceFilter filter)
        {
            return Ok(await _deviceService.GetFilteredDevices(filter));
        }
    }
}
