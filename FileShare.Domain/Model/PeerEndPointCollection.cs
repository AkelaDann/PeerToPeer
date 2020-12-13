using System.Net;
using System.Net.PeerToPeer;

namespace FileShare.Domain.Model
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName Peer , IPEndPointCollection iPEndPoint)
            {
            PeerHostName = Peer;
            PeerEndPoint = iPEndPoint;
            }
        public PeerName PeerHostName { get; } 
        public IPEndPointCollection PeerEndPoint { get; }
    }
}
