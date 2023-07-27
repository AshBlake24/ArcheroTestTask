using Source.Utilities;
using UnityEngine;

namespace Source.Combat
{
    public interface IPoolable<T> where T : Component, IPoolable<T>
    {
        void SetPool(ObjectPool<T> pool);
    }
}