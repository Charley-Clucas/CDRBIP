using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.UnitTests.Mocks;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CDRBIP.UnitTests.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    public class GetByReferenceTests
    {
        private readonly Mock<ICallDetailRecordRepository> _callDetailRecordRepository;

        public GetByReferenceTests()
        {
            _callDetailRecordRepository = MockCallDetailRecordRepository.GetCallDetailRecordRepository();
        }

        [Fact]
        public async Task Handle_WhenCorrectQueryObjectIsProvided_ExpectedObjectIsReturned()
        {
            //Arrange
            var handler = new GetByReference.Handler(_callDetailRecordRepository.Object);

            //Act
            var result = await handler.Handle(new GetByReference.Query(), new CancellationToken());

            //Assert
            result.ShouldNotBeNull();
        }
    }
}
