using System.Collections.ObjectModel;
using System.Net;
using System.Net.PeerToPeer;

namespace FileShare.Domain.Model
{
    public class PeerEndPointsCollection
    {
        public PeerEndPointsCollection(PeerName Peer)
        {
            PeerHostName = Peer;
            PeerEndPoints = new ObservableCollection<PeerEndPointInfo>();
            }
        public PeerName PeerHostName { get; set; }
        public ObservableCollection<PeerEndPointInfo> PeerEndPoints { get; set; }
        
    }
}
