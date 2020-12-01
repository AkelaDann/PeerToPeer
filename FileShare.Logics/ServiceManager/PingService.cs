using FileShare.Contract.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Logics.ServiceManager
{
    public class PingService : IPingServices
    {
        public void Ping(int Port, string peerUri)
        {
            Console.WriteLine($"yay ! from :{peerUri}");
        }
    }
}
