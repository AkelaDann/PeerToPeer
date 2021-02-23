using FileShare.Contract.Services;
using FileShare.Domain.Model;
using System;
using System.Net;
using System.Net.PeerToPeer;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FileShare.Logics.ServiceManager
{
    public class PeerConfigurationService : IPeerConfigurationService<PingService>
    {
        #region fields 
        private int port;
        private ICommunicationObject Communication;
        private DuplexChannelFactory<IPingServices> _factory;
        private bool _isServiceStarted;
        #endregion

        #region Cto

        public PeerConfigurationService(Peer<IPingServices> peer)
        {
            Peer = peer;
            PingServices = new PingService();
        }

        #endregion

        //public int Port => FindFreePort();

        private int FindFreePort()
        {
            int port;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                socket.Bind(endPoint);
                IPEndPoint local = (IPEndPoint)socket.LocalEndPoint;
                port = local.Port;
            }
            if (port == 0)
            {
                throw new ArgumentNullException(nameof(port));
            } 
                
            return port;
        }

        public Peer<IPingServices> Peer { get; }

        //int IPeerConfigurationService.port => FindFreePort();

        int IPeerConfigurationService<PingService>.port => FindFreePort();

        public PingService PingServices { get ; set ; }

        public bool StartPeerService()
        {
#pragma warning disable 618
            var binding = new NetPeerTcpBinding
            {
                Security = { Mode = SecurityMode.None }
            };
#pragma warning restore 618
            var endPoint = new ServiceEndpoint(ContractDescription.GetContract(typeof(IPingServices)), binding, new EndpointAddress("Net.p2p://FileShare"));
            Peer.Host = PingServices;
            _factory = new DuplexChannelFactory<IPingServices>(new InstanceContext(Peer.Host), endPoint);
            Peer.Channel = _factory.CreateChannel();
            Communication = (ICommunicationObject) Peer.Channel;
            if(Communication != null)
            {
                Communication.Opened += CommunicationOnOpened;
                try
                {
                    Communication.Open();
                    if (_isServiceStarted)
                        return _isServiceStarted;
                }
                catch(PeerToPeerException e)
                {
                    throw new PeerToPeerException("error estableciendo los servicios Peer: "+e);
                }
            }
            return _isServiceStarted;
        }

        private void CommunicationOnOpened(object sender, EventArgs e)
        {
            _isServiceStarted = true;   
        }


        public bool StopPeerService()
        {
           if(Communication != null)
            {
                Communication.Close();
                Communication = null;
                _factory = null;
                return true;
            }
            return false;
        }
    }
}
