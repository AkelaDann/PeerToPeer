using FileShare.Contract.FileShare;
using FileShare.Domain.FileSearch;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Logics.FileShareManager
{
    public delegate void CurrentHostInfo(HostInfo info);

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class FileShareManager : IFileShareService
    {
        
        private Dictionary<string, HostInfo> _currentHost = new Dictionary<string, HostInfo>();

        public event CurrentHostInfo CurrentHostUpdate;


        public void FowardResult(FileSearchResultModel Result)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetAllFileByte(FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public void PingHostService(HostInfo info)
        {
            Console.WriteLine($"Peer:{info.Id}    server:{info.Uri}:{info.Port}\n");
            var callback = OperationContext.Current.GetCallbackChannel<IFileShareServiceCallBack>();

            if (callback != null)
            {
                if (callback.IsConnected($"Message From Server at:{DateTime.UtcNow:D}"))
                {
                    _currentHost.Add(info.Id, info);
                    CurrentHostUpdate?.Invoke(info);
                }
            }
        }
    }
}
