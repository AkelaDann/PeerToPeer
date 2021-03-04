using System.Runtime.Serialization;

namespace FileShare.Domain.Model
{
    [DataContract]
    public partial class FileMetaData
    {
        public FileMetaData(string fileId, string fileName, int fileLenght )
        {
            FileId = fileId;
            FileName = fileName;
            FileLenght = fileLenght;

        }

        [DataMember]
        public string FileId { get;   }
        [DataMember]
        public string FileName { get;   }
        [DataMember]
        public int FileLenght { get;   }
    }
}
