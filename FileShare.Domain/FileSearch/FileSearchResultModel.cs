using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Domain.FileSearch
{
    public class FileSearchResultModel
    {
        public HostInfo ServisceHost { get; set; }
        public ObservableCollection<FileMetaData> File { get; set; }
    }
}
