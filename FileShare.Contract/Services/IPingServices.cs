using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Contract.Services
{
    [ServiceContract(CallbackContract = typeof(IPingServices))]
    interface IPingServices
    {
        [OperationContract(IsOneWay = true)]
        void Ping(int Port, string peerUri);
    }
}
