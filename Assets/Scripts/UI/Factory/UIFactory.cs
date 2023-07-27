using Source.Infrastructure.Assets;
using Source.Logic;
using Source.UI.Elements;
using UnityEngine;

namespace Source.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        
        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void CreateCountdownTimer(Timer timer) =>
            _assetProvider.Instantiate(AssetPath.CountdownTimer, _uiRoot)
                .GetComponent<TimerView>()
                .Construct(timer);

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}