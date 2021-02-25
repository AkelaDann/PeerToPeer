using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.SampleData
{
    public static class FileSample
    {
        private static ObservableCollection<File> _avaliableFiles= new ObservableCollection<File>();
        private static ObservableCollection<FileMetaData> _metaDatas = new ObservableCollection<FileMetaData>();

        public static ObservableCollection<File> GetAvaliableFiles()
        {
            if (!_avaliableFiles.Any())
            {
                _avaliableFiles.Add(new Task<File>(() => 
                {
                    var bytes = new byte[4654684];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/MP4"
                    };
                    _metaDatas.Add(file.GetFileMeta());
                    return file;
                }).Result);
                _avaliableFiles.Add(new Task<File>(() =>
                {
                    var bytes = new byte[4654685];
                    var file = new File
                    {
                        FileId = Guid.NewGuid().ToString().Split('-')[4],
                        FileName = "Max Payne 2",
                        FileContent = bytes,
                        FileLenght = bytes.Length,
                        FileType = "video/MP4"
                    };
                    _metaDatas.Add(file.GetFileMeta());
                    return file;
                }).Result);
            }

            return _avaliableFiles;
        }
    }
}
