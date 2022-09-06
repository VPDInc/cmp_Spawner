using UnityEngine;
using UnityEngine.Events;
using Spawners.Factories;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.5.0
namespace Spawners.Mono
{
    public abstract class Spawner<TComponent, TFactory> : MonoBehaviour, ISpawnable<TComponent> 
        where TComponent : Component
        where TFactory : class, IFactory<TComponent>
    {
        [Header("Controllers")]
        [SerializeField] private Core.Spawner<TComponent, TFactory> _spawner;

        public event UnityAction<TComponent> Spawned
        {
            add => _spawner.Spawned += value;
            remove => _spawner.Spawned -= value;
        }

        public void Spawn() => _spawner.Spawn();
    }
}
