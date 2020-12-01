using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Contract.Services
{
    public interface IPeerConfigurationService
    {
        int port { get; }
        Peer<IPingServices> Peer { get; }
        bool StartPeerService();
        bool StopPeerService();
    }
}
