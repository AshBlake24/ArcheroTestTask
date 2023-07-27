using Source.Infrastructure.Services;

namespace Source.Data.Service
{
    public interface IPersistentDataService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
        void Reset();
    }
}