namespace Source.Infrastructure.Services
{
    public interface ISceneLoadingService : IService
    {
        void Load(string level);
    }
}