using UnityEngine;
using UnityEngine.Events;
using Spawners.Factories;
using Spawners.Point.Getters;
using Spawners.Point.Setters;
using Spawners.Factories.Generic.Mono;
using Spawners.Point.Getters.Generic.Mono;
using Spawners.Point.Setters.Generic.Mono;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Generic
{
    public class ComponentSpawner : ComponentSpawner<Component> { }
    
    public class ComponentSpawner<TComponent> : Spawner<TComponent> where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] private BaseCreator<TComponent> _creator;
        [SerializeField] private BasePointGetter _getter;
        [SerializeField] private PointSetter _setter;
        #endregion

        #region Properties
        protected sealed override IFactory<TComponent> Factory => _creator;
        protected sealed override IGettablePoint Gettable => _getter;
        protected sealed override ISettablePoint Settable => _setter;
        protected sealed override UnityAction<TComponent> Initialize => InitializeObj;
        #endregion
        
        protected virtual void InitializeObj(TComponent obj) { }
    }
}
