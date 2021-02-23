using FileShare.Contract.Repository;
using FileShare.Domain.Model;
using System;
using System.Linq;
using System.Net.PeerToPeer;
using System.Net.PeerToPeer.Collaboration;


namespace FileShare.Logics.PnrpManager
{
    public class PeerNameResolver : IPeerNameResolverRepository
    {
        private PeerEndPointCollection _peers;
        private string _username;

        public PeerNameResolver(string username)
        {
            _username = username;
        }

        public PeerEndPointsCollection PeerEndPointCollection { get; set; }

        public void ResolvePeerName(string peerId)
        { 
            if (string.IsNullOrEmpty(_username))
                throw new ArgumentNullException(nameof(_username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            //PeerNameResolver resolvered = new PeerNameResolver(_username);
            var result = resolver.Resolve(new PeerName(peerId, PeerNameType.Unsecured), Cloud.AllLinkLocal); 
            //var resulted = resolvered.ResolvePeerName
            
            //if (result.Any())
            //    PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
        }

        public void ResolvePeerName()
        {
            throw new NotImplementedException();
        }
    }
}

