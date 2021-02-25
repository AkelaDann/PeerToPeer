namespace FileShare.Domain.Model
{
    public partial class FileMetaData
    {
        public FileMetaData(string fileId, string fileName, int fileLenght )
        {
            FileId = fileId;
            FileName = fileName;
            FileLenght = fileLenght;

        }

        public string FileId { get;   }
        public string FileName { get;   }
        public int FileLenght { get;   }
    }
}
