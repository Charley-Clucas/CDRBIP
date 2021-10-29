using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Dtos;
using CDRBIP.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CDRBIP.UnitTests.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetCallCountAndDurationTests
    {
        private readonly Mock<ICallDetailRecordRepository> _callDetailRecordRepository;

        public GetCallCountAndDurationTests()
        {
            _callDetailRecordRepository = MockCallDetailRecordRepository.GetCallDetailRecordRepository();
        }

        [Fact]
        public async Task Handle_WhenCorrectQueryObjectIsProvided_ExpectedObjectIsReturned()
        {
            //Arrange
            var handler = new GetCallCountAndDuration.Handler(_callDetailRecordRepository.Object);

            //Act
            var result = await handler.Handle(new GetCallCountAndDuration.Query(), new CancellationToken());

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<CallCountAndDurationDto>>();
            result.Count.ShouldBe(2);
        }
    }
}
