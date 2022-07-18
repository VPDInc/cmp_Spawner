using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners
{
    public interface ISpawnable<TComponent> where TComponent : Component
    {
        public event UnityAction<TComponent> Spawned;
        
        public void Spawn();
    }
}
