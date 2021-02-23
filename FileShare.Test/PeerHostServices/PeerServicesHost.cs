using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Domain.Model;
using FileShare.Logics.ServiceManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileShare.Test.PeerHostServices
{
    public class PeerServicesHost
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public PeerServicesHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService>  peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
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
                    
                    peer.Channel.Ping(ConfigurePeer.port, RegisterPeer.PeerUri);
                    
                    
                    if(ConfigurePeer != null)
                    {
                        ConfigurePeer.PingServices.PeerEndPointInformation += PingServicesOnPeerEndPointInformation;
                    }
                }
                else
                {
                    Console.WriteLine("error starting up peer services");
                }
                
            }
        }

        private void PingServicesOnPeerEndPointInformation(PeerEndPointInfo endPointInfo)
        {
            if(endPointInfo != null)
            {
                Console.WriteLine($"New Peer EndPoint{endPointInfo.PeerUri}");
                endPointInfo.PeerIpColletion.ToList()?.ForEach(p => Console.WriteLine($"\t\t\t IP:{p.Address}:{p.Port}"));
            }
        }
    }
}
