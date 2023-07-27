using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Utilities
{
    public class ObjectPool<T> where T : Component
    {
        private readonly GameObject _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _pool = new Queue<T>();
        
        private static Transform s_generalPoolsContainer;

        public ObjectPool(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            if (prefab.GetComponent<T>() == null)
                throw new ArgumentNullException(nameof(T));

            if (s_generalPoolsContainer == null)
                s_generalPoolsContainer = new GameObject($"Pools").transform;
            
            _prefab = prefab;

            _container = new GameObject($"Pool - {_prefab.name}").transform;
            _container.SetParent(s_generalPoolsContainer);
        }

        public void AddInstance(T instance)
        {
            if (instance.transform.parent != _container)
                instance.transform.SetParent(_container);

            instance.gameObject.SetActive(false);
            
            _pool.Enqueue(instance);
        }

        public T GetInstance() => _pool.Count <= 0 
            ? CreateInstance() 
            : _pool.Dequeue();

        private T CreateInstance()
        {
            GameObject instance = Object.Instantiate(_prefab, _container);
            T instanceComponent = instance.GetComponent<T>();
            return instanceComponent;
        }
    }
}