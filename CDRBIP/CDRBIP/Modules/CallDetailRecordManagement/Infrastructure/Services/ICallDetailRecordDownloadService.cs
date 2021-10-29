using System.IO;

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
