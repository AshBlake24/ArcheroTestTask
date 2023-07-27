using Source.Infrastructure.Services.StaticData;
using Source.Infrastructure.States;

namespace Source.Data.Service
{
    public class PersistentDataService : IPersistentDataService
    {
        private readonly IStaticDataService _staticData;

        public PersistentDataService(IStaticDataService staticData)
        {
            _staticData = staticData;
        }

        public PlayerProgress PlayerProgress { get; set; }
    }
}