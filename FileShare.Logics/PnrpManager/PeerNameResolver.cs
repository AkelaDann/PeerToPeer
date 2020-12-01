using FileShare.Contract.Repository;
using System.Net.PeerToPeer.Collaboration;
using System;
using System.Net.PeerToPeer;
using System.Linq;
using FileShare.Domain.Model;

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

        public void ResolvePeerName()
        {
            if (string.IsNullOrEmpty(_username))
                throw new ArgumentNullException(nameof(_username));

            System.Net.PeerToPeer.PeerNameResolver resolver = new System.Net.PeerToPeer.PeerNameResolver();
            var result = resolver.Resolve(new PeerName(_username), Cloud.AllLinkLocal);
            if (result.Any())
                PeerEndPointCollection = new PeerEndPointsCollection(result[0].PeerName, result[0].EndPointCollection);
        }
            
        
    }
}

