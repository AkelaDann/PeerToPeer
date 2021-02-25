using FileShare.Contract.FileShare;
using FileShare.Domain.FileSearch;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.Logics.FileShareManager
{
    public class FileManager : IFileShareService
    {
        public void FowardResult(FileSearchResultModel Result)
        {
            throw new NotImplementedException();
        }

        public FileMetaData.FilePartModel GetAllFileByte(FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }

        public FileMetaData.FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta)
        {
            throw new NotImplementedException();
        }
    }
}
