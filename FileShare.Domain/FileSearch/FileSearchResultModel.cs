using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domain.FileSearch
{
    [DataContract]
    public class FileSearchResultModel
    {
        [DataMember]
        public HostInfo ServisceHost { get; set; }
        [DataMember]
        public ObservableCollection<FileMetaData> Files { get; set; }
    }
}
