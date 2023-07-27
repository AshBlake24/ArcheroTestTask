using UnityEngine;

namespace Source.Infrastructure.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            CheckGameObject(path, prefab);
            
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 position)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            CheckGameObject(path, prefab);
            
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            CheckGameObject(path, prefab);
            
            return Object.Instantiate(prefab, parent);
        }
        
        private static void CheckGameObject(string path, Object prefab)
        {
            if (prefab == null)
                throw new System.ArgumentNullException($"Object at path {path} does not exist");
        }
    }
}