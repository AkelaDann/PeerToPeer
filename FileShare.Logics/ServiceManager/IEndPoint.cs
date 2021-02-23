using System.Net;

namespace FileShare.Logics.ServiceManager
{
    internal class IEndPoint : IPEndPoint
    {
        public IEndPoint(IPAddress address, int port) : base(address, port)
        {
        }
    }
}