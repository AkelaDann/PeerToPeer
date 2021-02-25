using FileShare.Contract.Services;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FileShare.Domain.FileSearch;

namespace FileShare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(HostInfo endPointInfo);

    public delegate void FileSearchResultDelegate(FileSearchResultModel fileSearch);
    public class PingService : IPingServices
    {
        public event OnPeerInfo PeerEndPointInformation;
        public event FileSearchResultDelegate FileSearchResult;
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

        

        public void SearchFiles(string searchTerm, string ClientHost)
        {
            throw new NotImplementedException();
        }
    }   
}
