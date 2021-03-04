using FileShare.Contract.Services;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.FileSearch;
using System.Collections.ObjectModel;

namespace FileShare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(HostInfo endPointInfo);

    public delegate void FileSearchResultDelegate(FileSearchResultModel fileSearch);
    public class PingService : IPingServices
    {
        public event OnPeerInfo PeerEndPointInformation;
        public event FileSearchResultDelegate FileSearchResult;

        public PingService()
        {

        }
        public PingService(HostInfo info)
        {
            FileServiceHost = info;
            ClientHostDetails = new ObservableCollection<HostInfo>();
        }


        public void Ping(HostInfo info)
        {
            var Host = Dns.GetHostEntry(info.Uri);
            IPEndPointCollection ips = new IPEndPointCollection();
            //Console.WriteLine($"new peer entered peer endpoint details :");
            Host.AddressList.ToList()?.ForEach(p => { ips.Add(new IEndPoint(p, info.Port)); });//Console.WriteLine($"\t \t \t Endpoint: {p}{Port}"));
            // Console.WriteLine($"yay ! from :{peerUri}");
            var peerInfo = new PeerEndPointInfo
            {
                LastUpdate = DateTime.UtcNow,
                PeerUri = info.Uri,
                PeerIpColletion = ips
            };
            PeerEndPointInformation?.Invoke(info);

        }

        public HostInfo FileServiceHost { get; set; }
        public ObservableCollection <FileMetaData> AvaliableFileMetaData { get; set; }
        public ObservableCollection<HostInfo> ClientHostDetails { get; set; }



        public void SearchFiles(string searchTerm, string peerId)   
        {
            if (ClientHostDetails.Any())
            {
                var info = ClientHostDetails.First(p => p.Id == peerId);
                var result = (from file in AvaliableFileMetaData
                              where searchTerm == file.FileName
                              select file);
                if (info != null)
                {
                    if(result.Any())
                    {
                        FileSearchResultModel search = new FileSearchResultModel
                        {
                            ServisceHost = FileServiceHost,
                            Files = (ObservableCollection<FileMetaData>)result
                        };
                    }
                }
            }
        }
    }   
}
