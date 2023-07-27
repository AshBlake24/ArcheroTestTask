using Source.Infrastructure.Services;
using Source.Logic;

namespace Source.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateCountdownTimer(Timer timer);
        void CreateUIRoot();
    }
}