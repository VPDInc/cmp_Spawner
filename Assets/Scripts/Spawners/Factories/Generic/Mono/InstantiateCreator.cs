using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Factories.Generic.Mono
{
    [AddComponentMenu("Spawners/Factories/Instantiate Creator")]
    public sealed class InstantiateCreator : InstantiateCreator<Component> { }
    
    public abstract class InstantiateCreator<TComponent> : BaseCreator<TComponent> where TComponent : Component
    {
        [SerializeField] private Core.InstantiateCreator<TComponent> _creator;

        public sealed override TComponent Create() => _creator.Create();
    }
}
