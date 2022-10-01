using UnityEngine;
using Spawners.Factories.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.5.0
namespace Spawners.Mono.Generic
{
    public sealed class InstanceTimeSpawner : InstanceTimeSpawner<Component> { }
    
    public abstract class InstanceTimeSpawner<TComponent> : TimeSpawner<TComponent, InstantiateCreator<TComponent>>
        where TComponent : Component { }
}
