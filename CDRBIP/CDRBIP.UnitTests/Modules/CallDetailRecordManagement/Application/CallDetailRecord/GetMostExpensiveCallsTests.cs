using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CDRBIP.UnitTests.Modules.CallDetailRecordManagement.Application.CallDetailRecordTests
{
    public class GetMostExpensiveCallsTests
    {
        private readonly Mock<ICallDetailRecordRepository> _callDetailRecordRepository;

        public GetMostExpensiveCallsTests()
        {
            _callDetailRecordRepository = MockCallDetailRecordRepository.GetCallDetailRecordRepository();
        }

        [Fact]
        public async Task Handle_WhenCorrectQueryObjectIsProvided_ExpectedObjectIsReturned()
        {
            //Arrange
            var handler = new GetMostExpensiveCalls.Handler(_callDetailRecordRepository.Object);

            //Act
            var result = await handler.Handle(new GetMostExpensiveCalls.Query(), new CancellationToken());

            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<CDRBIP.Modules.CallDetailRecordManagement.Domain.CallDetailRecord>>();
            result.Count.ShouldBe(1);
        }
    }
}
