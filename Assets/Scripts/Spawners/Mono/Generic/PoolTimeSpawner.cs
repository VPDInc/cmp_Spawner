using UnityEngine;
using Spawners.Factories.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.5.0
namespace Spawners.Mono.Generic
{
    public sealed class PoolTimeSpawner: PoolTimeSpawner<Component> { }
    
    public abstract class PoolTimeSpawner<TComponent> : TimeSpawner<TComponent, PoolCreator<TComponent>>
        where TComponent : Component { }
}
