using System;
using Pool.Mono;
using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.5.0
namespace Spawners.Factories.Generic
{
    [Serializable]
    public sealed class PoolCreator<TComponent> : IFactory<TComponent>  where TComponent : Component
    {
        [SerializeField] private ObjectPool<TComponent> _pool;

        public TComponent Create(UnityAction<TComponent> initialize) => _pool.Get(initialize);
    }
}
