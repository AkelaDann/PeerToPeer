using System.Net.PeerToPeer.Collaboration;

namespace FileShare.Contract.Repository
{
    public interface IPeerNameResolverRepository
    {
        void ResolvePeerName();
        PeerEndPointCollection PeerEndPointCollection { get; set; }
    }
}
