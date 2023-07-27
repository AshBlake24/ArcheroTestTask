using System;
using System.Collections.Generic;
using Source.Combat;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Utilities
{
    public class ObjectPool<T> where T : Component, IPoolable<T>
    {
        private readonly GameObject _prefab;
        private readonly Queue<T> _pool = new Queue<T>();
        
        private static Transform s_container;

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            if (prefab.GetComponent<T>() == null)
                throw new ArgumentNullException(nameof(T));
            
            _prefab = prefab;
            
            if (s_container == null)
                s_container = new GameObject($"Pool - {_prefab.name}").transform;
            
            s_container.SetParent(Helpers.GetGeneralPoolsContainer());
        }

        public void Release(T instance)
        {
            if (instance.transform.parent != s_container)
                instance.transform.SetParent(s_container);

            instance.gameObject.SetActive(false);
            
            _pool.Enqueue(instance);
        }

        public T Get() => _pool.Count <= 0 
            ? CreateInstance() 
            : _pool.Dequeue();

        private T CreateInstance()
        {
            GameObject instance = Object.Instantiate(_prefab, s_container);
            T instanceComponent = instance.GetComponent<T>();
            instanceComponent.SetPool(this);
            return instanceComponent;
        }
    }
}