using Microsoft.Extensions.Configuration;
using System.IO;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Services
{
    public class CallDetailRecordDownloadService : ICallDetailRecordDownloadService
    {
        private readonly IConfiguration _configuration;
        private string cdrDirectory;
        private string cdrBadFileDirectory;

        public CallDetailRecordDownloadService(IConfiguration configuration)
        {
            _configuration = configuration;

            cdrDirectory = _configuration.GetSection("CDRFileConfiguration").GetValue<string>("Directory");
            cdrBadFileDirectory = _configuration.GetValue<string>("CDRFileConfiguration:BadFileDirectory");
        }

        public StreamReader DownloadCallDetailRecordFile()
        {
            return new StreamReader(cdrDirectory);
        }

        public bool DoesFileExist()
        {
            if (!File.Exists(cdrDirectory))
            {
                return false;
            }
            return true;
        }

        public StreamWriter UploadCallDetailRecordFailuresFile()
        {
            var badFileWriter =  new StreamWriter(cdrBadFileDirectory);

            return badFileWriter;
        }

        public void DeleteFile()
        {
            File.Delete(cdrDirectory);
        }
    }
}
