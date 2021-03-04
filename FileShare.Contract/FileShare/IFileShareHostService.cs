using System.ServiceModel;

namespace FileShare.Contract.FileShare
{
    [ServiceContract(CallbackContract = typeof( IFileShareServiceCallBack), SessionMode = SessionMode.Required)  ]
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }
}
