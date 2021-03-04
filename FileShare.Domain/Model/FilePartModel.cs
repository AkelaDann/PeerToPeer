using System.Runtime.Serialization;

namespace FileShare.Domain.Model
{

    //public partial class FileMetaData
    //{
    [DataContract]
    public class FilePartModel
    {
        private readonly File _file; 
        public FilePartModel(File file)
        {
            _file = file;  
        }
        
        [DataMember]
        public FilePart FilePart { get; set; }
        [DataMember]
        public string FileId => _file.FileId; 
        [DataMember]
        public byte[] FileBytes { get; set; }
    }
   // }
}
