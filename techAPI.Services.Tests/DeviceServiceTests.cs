using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace techAPI.Services.Tests
{
    public class DeviceServiceTests
    {
        private Mock<IHistoryRepository> _historyRepo;
        private IHistoryRepository _mockhistoryRepository;
        private HistoryService _historyService;
        private IMapper _mapper;
        private List<History> _fakeHistoryDtos;
    }
}
