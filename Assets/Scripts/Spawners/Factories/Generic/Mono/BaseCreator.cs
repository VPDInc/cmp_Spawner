using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners.Factories.Generic.Mono
{
    public abstract class BaseCreator<TComponent> : MonoBehaviour, IFactory<TComponent> where TComponent : Component
    {
        public abstract TComponent Create(UnityAction<TComponent> initialize);
    }
}
