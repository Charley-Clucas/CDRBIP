using CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Database;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Application.CallDetailRecord
{
    //ToDo: File retrieval encapsulation in a seperate class so its easy to switch to AWS SDK retrieval for s3 bucket.
    //ToDo: Encapsulate domain login within domain class.
    public class RetrieveCDRFile
    {
        public class Command : IRequest<int>
        {
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly ICallDetailRecordContext _callDetailRecordContext;

            public Handler(ICallDetailRecordContext callDetailRecordContext)
            {
                _callDetailRecordContext = callDetailRecordContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                if (!File.Exists(@"SampleCsv\techtest_cdr_dataset.csv")) 
                {
                    //Log to serilog that we looked for a file however none exists at this time.
                    return 0;
                }

                var goodRecords = new List<Domain.CallDetailRecord>();
                var badRecords = new List<string>();
                var isRecordBad = false;

                using (var reader = new StreamReader(@"SampleCsv\techtest_cdr_dataset.csv"))
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
                        using (var writer = new StreamWriter(@"SampleCsv\techtest_cdr_dataset_bad_records.csv"))
                        {
                            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csvWriter.WriteHeader<Domain.CallDetailRecord>();
                                csvWriter.NextRecord();

                                foreach (var record in badRecords)
                                {
                                    csvWriter.WriteField(record);
                                    csvWriter.NextRecord();
                                }
                            }
                        }
                        try
                        {
                            await _callDetailRecordContext.CallDetailRecords.AddRangeAsync(goodRecords, cancellationToken);
                            await _callDetailRecordContext.SaveChangesAsync(cancellationToken);
                        }
                        catch (System.Exception x)
                        {
                            throw x;
                        }
                    }
                }
                File.Delete(@"SampleCsv\techtest_cdr_dataset.csv");
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
