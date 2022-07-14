using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.0
namespace Pool
{
    public interface IPooledObject<TComponent>
    {
        public event UnityAction<TComponent> ReturnedToPool;
    }
}
