using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileShare.Desktop.PeerHostServices
{
    public class PeerServiceHost
    {
        private AutoResetEvent _resetEvent = new AutoResetEvent(false);
        public PeerServiceHost(IPeerRegistrationRepository peerRegistration, IPeerNameResolverRepository peerNameResolver, IPeerConfigurationService peerConfigurationService)
        {
            RegisterPeer = peerRegistration;
            ResolvePeer = peerNameResolver;
            ConfigurePeer = peerConfigurationService;
        }

        public IPeerRegistrationRepository RegisterPeer { get; set;}
        public IPeerNameResolverRepository ResolvePeer { get; set;}
        public IPeerConfigurationService ConfigurePeer { get; set; }

        public void RunPeerServiceHost(Peer<IPingServices> peer) 
        {
            if (peer == null)
                throw new ArgumentNullException(nameof(peer));

            RegisterPeer.StartPeerRegistration(peer.UserName, ConfigurePeer.port);
            if (RegisterPeer.IsPeerRegistered)
            {
                Console.WriteLine($"PeerResgistrationCompleted");
                Console.WriteLine($"Peer Uri : {RegisterPeer.PeerUri} Port: {ConfigurePeer.port}");
            }
             if(ResolvePeer != null)
            {
                Console.WriteLine($"Resolving peer");
            }
        }

    }
}
