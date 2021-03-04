namespace FileShare.Contract.FileShare
{
    public interface IFileShareServiceCallBack
    {
        bool IsConnected(string replyMessage);
    }
}
