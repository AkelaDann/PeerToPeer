using FileShare.Contract.FileShare;
using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Domain.Model;
using FileShare.Logics.FileShareManager;
using FileShare.Logics.ServiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileShare.Test.PeerHostServices
{
    public class PeerServicesHost
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        bool isStarted = false;
        private int _port = 0;
        FileShareManager _file = new FileShareManager();
        Dictionary<string, HostInfo> _currentHost= new Dictionary<string, HostInfo>();

        public PeerServicesHost(IPeerRegistrationRepository peerRegistration,
            IPeerNameResolverRepository peerNameResolver, 
            IPeerConfigurationService<PingService>  peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
            _port = ConfigurePeer.port;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }
             
        public void RunPeerServiceHost(Peer<IPingServices> peer)
        {
            if (peer == null)
                throw new ArgumentNullException(nameof(peer));

            RegisterPeer.StartPeerRegistration(peer.PeerId, ConfigurePeer.port);
            if (RegisterPeer.IsPeerRegistered)
            {
                Console.WriteLine($"{peer.UserName } Peer Registration Completed");
                Console.WriteLine($"Peer Uri : {RegisterPeer.PeerUri}   Port : { ConfigurePeer.port}");
            }
            if (ResolvePeer != null)
            {
                Console.WriteLine($"resolving peer {peer.UserName}");
                ResolvePeer.ResolvePeerName(peer.PeerId);

                var result = ResolvePeer.PeerEndPointCollection;

                Console.WriteLine($"\t\t EndPoints for {RegisterPeer.PeerUri}");
                if (ConfigurePeer.StartPeerService())
                {
                    Console.WriteLine("Peer services started");
                    
                    peer.Channel.Ping(new HostInfo
                    {
                        Id = peer.PeerId,
                        Port = ConfigurePeer.port,
                        Uri = RegisterPeer.PeerUri 
                    });
                    Console.WriteLine("press enter to view endPoints");
                    
                    if(ConfigurePeer != null)
                    {
                        ConfigurePeer.PingServices.PeerEndPointInformation += PingServicesOnPeerEndPointInformation;
                    }
                    if (StartFileShareService(_port,RegisterPeer.PeerUri)) 
                    {
                        Console.WriteLine("File service host started");
                    };
                }
                else
                {
                    Console.WriteLine("error starting up peer services");
                }
                
            }
        }

        private void PingServicesOnPeerEndPointInformation(HostInfo endPointInfo)
        {
            Console.Clear();
            var uri = $"net.tcp://{endPointInfo.Uri}:{endPointInfo.Port}/FileShare";
        }

        public bool StartFileShareService(int port,  string uri)
        {
            if(uri.Any() && _port >0)
            {
                Uri[] uris = new Uri[1];
                var address = $"net.tcp://{uri}:{port}/Fileshare";
                uris[0] = new Uri(address);
                IFileShareService fileshare = _file;
                var host = new ServiceHost(fileshare, uris);
                var binding = new NetTcpBinding(SecurityMode.None);
                host.AddServiceEndpoint(typeof(IFileShareService), binding, string.Empty );
                host.Opened += HostOnOpened;
                _file.CurrentHostUpdate += FileOnCurrentHostUpDate;
                host.Open();
                return isStarted;
            }
            return false;
        }

        private void FileOnCurrentHostUpDate(HostInfo info)
        {
            Console.Clear();
            if(info != null && _currentHost.All(p => p.Key != info.Id))
            {
                _currentHost.Add(info.Id, info);
                Console.WriteLine($"{_currentHost.Count} Host currently avaliable");
                _currentHost.ToList().ForEach(p =>
                {
                    Console.WriteLine($"Host ID: {p.Key} EndPoint: {p.Value.Uri}:{p.Value.Port}");
                });
            }
            else if (!_currentHost.Any() )
            {
                _currentHost.Add(info.Id, info);
                Console.WriteLine($"{_currentHost.Count} Host currently avaliable");
                _currentHost.ToList().ForEach(p =>
                {
                    Console.WriteLine($"Host ID: {p.Key} EndPoint: {p.Value.Uri}:{p.Value.Port}");
                });
            }
        }

        private void HostOnOpened(object sender, EventArgs e)
        {
            isStarted = true; 
        }
    }
}
