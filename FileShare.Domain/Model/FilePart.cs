using System.Runtime.Serialization;

namespace FileShare.Domain.Model
{
    [DataContract]
    public class FilePart
    {
        public FilePart(int take, int skip =0)
        {
            Take = take;
            Skip = skip;
        }

        [DataMember]
        public int Take { get; }
        [DataMember]
        public int Skip { get;  set; }
    }
}
