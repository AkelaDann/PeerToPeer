using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Contract.Services
{
    [ServiceContract(CallbackContract = typeof(IPingServices))]
    public interface IPingServices
    {
        [OperationContract(IsOneWay = true)]
        void Ping(HostInfo host);

        [OperationContract(IsOneWay = true)]
        void SearchFiles(string searchTerm, string ClientHost);
    }
}
