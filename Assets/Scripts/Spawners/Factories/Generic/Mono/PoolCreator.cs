using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Factories.Generic.Mono
{
    [AddComponentMenu("Spawners/Factories/Pool Creator")]
    public sealed class PoolCreator : PoolCreator<Component> { }

    public abstract class PoolCreator<TComponent> : BaseCreator<TComponent> where TComponent : Component
    {
        [SerializeField] private Core.PoolCreator<TComponent> _creator;

        public override TComponent Create(UnityAction<TComponent> initialize) => _creator.Create(initialize);
    }
}
