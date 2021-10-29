using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CDRBIP.Modules.CallDetailRecordManagement.Domain;
using CDRBIP.Modules.CallDetailRecordManagement.Domain.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CDRBIP.UnitTests.Mocks
{
    public static class MockCallDetailRecordRepository
    {
        public static Mock<ICallDetailRecordRepository> GetCallDetailRecordRepository()
        {
            var callDetailRecords1 = new List<CallDetailRecord>
            {
                new CallDetailRecord
                {
                    CallerId = 441216000000,
                    Recipient = "4480000000320",
                    CallDate = DateTime.UtcNow,
                    EndTime = DateTime.Now,
                    Duration = 10,
                    Cost = 10,
                    Reference = "C5DA9724701EEBBA95CA2CC5617BA93EG",
                    Currency = "GBP",
                    Type = CallType.International
                },
                new CallDetailRecord
                {
                    CallerId = 441216000000,
                    Recipient = "4480000000320",
                    CallDate = DateTime.UtcNow,
                    EndTime = DateTime.Now,
                    Duration = 20,
                    Cost = 200,
                    Reference = "C5DA9724701EEBBA95CA2CC5617BA93QF",
                    Currency = "GBP",
                    Type = CallType.International
                }
            };

            var callDetailRecords2 = new List<CallDetailRecord> {
               new CallDetailRecord
                {
                    CallerId = 441216000001,
                    Recipient = "4480000000330",
                    CallDate = DateTime.UtcNow,
                    EndTime = DateTime.Now,
                    Duration = 10,
                    Cost = 10,
                    Reference = "C5DA9724701EEBBA95CA2CC5617BA93PO",
                    Currency = "GBP",
                    Type = CallType.Domestic
                }
            };

            var callCountDto = new List<CallCountAndDurationDto>
            {
                new CallCountAndDurationDto
                {
                    TotalCount = callDetailRecords1.Count,
                    TotalDuration = callDetailRecords1.Select(x => x.Duration).Sum()
                },
                new CallCountAndDurationDto
                {
                    TotalCount = callDetailRecords2.Count,
                    TotalDuration = callDetailRecords2.Select(x => x.Duration).Sum()
                }
            };

            var mockRepo = new Mock<ICallDetailRecordRepository>();

            mockRepo.Setup(r => r.GetAllByCallerId(It.IsAny<long>(), new CancellationToken())).ReturnsAsync(callDetailRecords1);

            mockRepo.Setup(r => r.GetByReference(It.IsAny<string>(), new CancellationToken())).ReturnsAsync(callDetailRecords2.Single());

            mockRepo.Setup(r => r.GetCallCountAndDuration(null, new CancellationToken())).ReturnsAsync(callCountDto);

            mockRepo.Setup(r => r.GetMostExpensiveCalls(It.IsAny<long>(), It.IsAny<int>(), null, new CancellationToken())).ReturnsAsync(callDetailRecords2);

            return mockRepo;
        }
    }
}
