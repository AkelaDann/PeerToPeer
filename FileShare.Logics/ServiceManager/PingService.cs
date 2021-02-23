using FileShare.Contract.Services;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Logics.ServiceManager
{
    public delegate void OnPeerInfo(PeerEndPointInfo endPointInfo);
    public class PingService : IPingServices
    {
        public event OnPeerInfo PeerEndPointInformation; 
        public void Ping(int Port, string peerUri)
        {
            var Host = Dns.GetHostEntry(peerUri);
            IPEndPointCollection ips = new IPEndPointCollection();
            //Console.WriteLine($"new peer entered peer endpoint details :");
            Host.AddressList.ToList()?.ForEach(p => { ips.Add(new IEndPoint(p, Port)); });//Console.WriteLine($"\t \t \t Endpoint: {p}{Port}"));
            // Console.WriteLine($"yay ! from :{peerUri}");
            var peerInfo = new PeerEndPointInfo
            {
                LastUpdate = DateTime.UtcNow,
                PeerUri = peerUri,
                PeerIpColletion = ips
            };
            PeerEndPointInformation?.Invoke(peerInfo);

        }
    }   
}
