using Source.Infrastructure.Services;
using UnityEngine;

namespace Source.Infrastructure.Assets
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 position);
        GameObject Instantiate(string path, Transform parent);
    }
}