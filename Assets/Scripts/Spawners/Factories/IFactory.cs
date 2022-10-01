using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.5.0
namespace Spawners.Factories
{
    public interface IFactory<TComponent> where TComponent : Component
    {
        public TComponent Create(UnityAction<TComponent> initialize);
    }
}
