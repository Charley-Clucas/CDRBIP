using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CDRBIP.UnitTests.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetAllByCallerIdHandlerTests
    {
        private readonly Mock<ICallDetailRecordRepository> _repositoryMock;

        public GetAllByCallerIdHandlerTests()
        {
            _repositoryMock = MockCallDetailRecordRepository.GetCallDetailRecordRepository();
        }

        [Fact]
        public async Task Handle_WhenCorrectQueryObjectIsProvided_ExpectedObjectIsReturned()
        {
            //Arrange
            var handler = new GetAllByCallerId.Handler(_repositoryMock.Object);

            //Act
            var result = await handler.Handle(new GetAllByCallerId.Query(), new CancellationToken());

            //Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }
    }
}
