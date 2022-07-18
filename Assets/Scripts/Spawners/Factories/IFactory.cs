using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners.Factories
{
    public interface IFactory<TComponent> where TComponent : Component
    {
        public TComponent Create(UnityAction<TComponent> initialize);
    }
}
