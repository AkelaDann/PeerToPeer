namespace FileShare.Contract.FileShare
{
    public interface IFileShareHostService
    {
        bool Stop();
        bool Start();
    }
}
