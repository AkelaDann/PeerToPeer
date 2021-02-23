using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Logics.ServiceManager;

namespace FileShare.Web.PnrpManager
{
    public class PeerServicesHost
    {
        public PeerServicesHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService<PingService> peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set; }
        public IPeerNameResolverRepository ResolvePeer { get; set; }
        public IPeerConfigurationService<PingService> ConfigurePeer { get; set; }
        public void RunPeerServices()
        {
            if(ConfigurePeer.Peer != null)
            {
                 
            }
        }
    }
}
