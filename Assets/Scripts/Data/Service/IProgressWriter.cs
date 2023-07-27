namespace Source.Data.Service
{
    public interface IProgressWriter : IProgressReader
    {
        void WriteProgress(PlayerProgress progress);
    }
}