using FileShare.Domain.FileSearch;
using FileShare.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static FileShare.Domain.Model.FileMetaData;

namespace FileShare.Contract.FileShare
{
    [ServiceContract]
    public interface IFileShareService
    {
        [OperationContract(IsOneWay = false)]
        FilePartModel GetAllFileByte(FileMetaData fileMeta);
        
        [OperationContract(IsOneWay = false)]
        FilePartModel GetFilePartBytes(FilePart filePart, FileMetaData fileMeta);

        [OperationContract(IsOneWay = false)]
        void FowardResult(FileSearchResultModel Result);

    }
}
