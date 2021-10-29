using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CDRBIP.Modules.CallDetailRecordManagement.Infrastructure.Services
{
    public interface ICallDetailRecordDownloadService
    {
        StreamReader DownloadCallDetailRecordFile();
        bool DoesFileExist();
        StreamWriter UploadCallDetailRecordFailuresFile();
        void DeleteFile();
    }
}
