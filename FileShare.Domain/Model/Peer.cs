using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domain.Model
{
    
    public class Peer <T>
    {
        public string PeerId { get; set; }
        public string UserName { get; set; }
        public PeerName PeerName { get; set; }
        public T Channel { get; set; }
        public T Host { get; set; }

    }
}
