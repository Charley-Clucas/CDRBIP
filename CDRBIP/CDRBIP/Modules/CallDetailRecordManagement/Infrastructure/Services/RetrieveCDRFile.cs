using CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using MediatR;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Services
{
    public class RetrieveCDRFile
    {
        public class Command : IRequest<int>
        {
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ICallDetailRecordRepository _callDetailRecordRepository;
            private readonly ICallDetailRecordDownloadService _callDetailRecordDownloadService;

            public Handler(ICallDetailRecordRepository callDetailRecordRepository, ICallDetailRecordDownloadService callDetailRecordDownloadService)
            {
                _callDetailRecordRepository = callDetailRecordRepository;
                _callDetailRecordDownloadService = callDetailRecordDownloadService;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!_callDetailRecordDownloadService.DoesFileExist()) 
                {
                    return 0;
                }

                var goodRecords = new List<Domain.CallDetailRecord>();
                var badRecords = new List<string>();
                var isRecordBad = false;

                using (var reader = _callDetailRecordDownloadService.DownloadCallDetailRecordFile())
                {
                    using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<CallDetailRecordMap>();
                        csvReader.Context.Configuration.DetectDelimiter = true;

                        while (csvReader.Read())
                        {
                            try
                            {
                                var record = csvReader.GetRecord<Domain.CallDetailRecord>();

                                if (!isRecordBad)
                                {
                                    goodRecords.Add(record);
                                }

                                isRecordBad = false;
                            }
                            catch (TypeConverterException x)
                            {
                                badRecords.Add(x.Context.Parser.RawRecord);
                            }
                        }
                        using (var writer = _callDetailRecordDownloadService.UploadCallDetailRecordFailuresFile())
                        {
                            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csvWriter.WriteHeader<Domain.CallDetailRecord>();
                                csvWriter.NextRecord();

                                var duplicateRecords = await _callDetailRecordRepository.SaveCDRFileRecords(goodRecords, cancellationToken);

                                foreach (var record in badRecords)
                                {
                                    csvWriter.WriteField(record);
                                    csvWriter.NextRecord();
                                }

                                foreach (var duplicateRecord in duplicateRecords)
                                {
                                    csvWriter.WriteRecord(duplicateRecord);
                                    csvWriter.NextRecord();
                                }
                            }
                        }
                    }
                }
                _callDetailRecordDownloadService.DeleteFile();
                return goodRecords.Count;

            }

            public class CallDetailRecordMap : ClassMap<Domain.CallDetailRecord>
            {
                public CallDetailRecordMap()
                {
                    var dateTimeFormat = "dd/MM/yyyy";

                    Map(m => m.CallerId).Name("caller_id");
                    Map(m => m.Recipient).Name("recipient");
                    Map(m => m.CallDate).Name("call_date").TypeConverterOption.Format(dateTimeFormat);
                    Map(m => m.EndTime).Name("end_time");
                    Map(m => m.Duration).Name("duration");
                    Map(m => m.Cost).Name("cost");
                    Map(m => m.Reference).Name("reference");
                    Map(m => m.Currency).Name("currency");
                    Map(m => m.Type).Name("type");
                }
            }
        }
    }
}
