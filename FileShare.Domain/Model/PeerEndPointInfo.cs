using System;
using System.Net;

namespace FileShare.Domain.Model
{
    public class PeerEndPointInfo
    {
        public string PeerUri { get; set; }
        public IPEndPointCollection PeerIpColletion { get; set; }

        public DateTime LastUpdate { get; set; }
    }
}