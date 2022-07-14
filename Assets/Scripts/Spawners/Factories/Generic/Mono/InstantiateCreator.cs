using UnityEngine;
using UnityEngine.Events;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Factories.Generic.Mono
{
    [AddComponentMenu("Spawners/Factories/Instantiate Creator")]
    public sealed class InstantiateCreator : InstantiateCreator<Component> { }
    
    public abstract class InstantiateCreator<TComponent> : BaseCreator<TComponent> where TComponent : Component
    {
        [SerializeField] private Core.InstantiateCreator<TComponent> _creator;

        public sealed override TComponent Create(UnityAction<TComponent> initialize) => _creator.Create(initialize);
    }
}
