using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Domain.Model;
using FileShare.Logics.PnrpManager;
using FileShare.Logics.ServiceManager;

namespace FileShare.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //   > < >
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() <= 1)
            {
                Process.Start("FileShare.Test.exe");
            }

            new Program().Run();


        }

        private void Run()
        {
            Peer<IPingServices> peer = new Peer<IPingServices> {UserName = Guid.NewGuid().ToString().Split('-')[4]};
            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.UserName);
            IPeerConfigurationService peerConfigurationService = new PeerConfigurationService(peer);
            peerRegistration.StartPeerRegistration(peer.UserName, peerConfigurationService.port);
            Console.WriteLine("Peer Information");
            Console.WriteLine($"Peer Uri : {peerRegistration.PeerUri}  port {peerConfigurationService.port}");
            var host = Dns.GetHostEntry(peerRegistration.PeerUri);
            host.AddressList?.ToList().ForEach(p => Console.WriteLine($"\t\t IP : {p}"));
            Console.ReadLine();
        }
    }
}
