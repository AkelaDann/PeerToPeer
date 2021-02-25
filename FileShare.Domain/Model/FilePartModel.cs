namespace FileShare.Domain.Model
{
    public partial class FileMetaData
    {
        public class FilePartModel
        {
            private readonly File _file; 
            public FilePartModel(File file)
            {
                _file = file;  
            }

            public FilePart FilePart { get; set; }
            public string FileId => _file.FileId; 
            public byte[] FileBytes { get; set; }
        }
    }
}
