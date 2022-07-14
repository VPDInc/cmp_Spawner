using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.0
namespace Pool
{
    public sealed class PooledObject : MonoBehaviour, IPooledObject<PooledObject>
    {
        public event UnityAction<PooledObject> ReturnedToPool;

        private void OnEnable() => ReturnedToPool?.Invoke(this);
    }
}
