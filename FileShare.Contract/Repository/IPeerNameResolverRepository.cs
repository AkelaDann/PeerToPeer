using FileShare.Domain.Model;
using System.Net.PeerToPeer.Collaboration;

namespace FileShare.Contract.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName(string peerId);
        PeerEndPointsCollection PeerEndPointCollection { get; set; }
    }
}
