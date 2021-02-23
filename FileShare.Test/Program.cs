using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FileShare.Contract.Repository;
using FileShare.Contract.Services;
using FileShare.Domain.Model;
using FileShare.Logics.PnrpManager;
using FileShare.Logics.ServiceManager;
using FileShare.Test.PeerHostServices;


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
            Console.WriteLine($"Hola ingrese su usuario:");
            string username = Console.ReadLine();

            Peer<IPingServices> peer = new Peer<IPingServices>
            {
                PeerId = Guid.NewGuid().ToString().Split('-')[4],
                UserName = username
            };

            IPeerRegistrationRepository peerRegistration = new PeerRegistrationManager();
            IPeerNameResolverRepository peerNameResolver = new PeerNameResolver(peer.PeerId);
            IPeerConfigurationService<PingService> peerConfigurationService = new PeerConfigurationService(peer);
            PeerServicesHost psh = new PeerServicesHost (peerRegistration,peerNameResolver,peerConfigurationService);
            Thread thd = new Thread( () => 
            {
                psh.RunPeerServiceHost(peer);
            }) {IsBackground = true };

            thd.Start();
            Console.ReadLine();
        }
    }
}
