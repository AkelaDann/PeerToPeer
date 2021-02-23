namespace FileShare.Domain.Model
{
    public class FilePart
    {
        public FilePart(int take, int skip =0)
        {
            Take = take;
            Skip = skip;
        }

        public int Take { get; }
        public int Skip { get;  set; }
    }
}
