using UnityEngine;
using Spawners.Factories.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.5.0
namespace Spawners.Mono.Generic
{
    public sealed class InstanceSpawner : InstanceSpawner<Component> { }
    
    public abstract class InstanceSpawner<TComponent> : Spawner<TComponent, InstantiateCreator<TComponent>>
        where TComponent : Component { }
}
