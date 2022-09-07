using System;
using UnityEngine;
using UnityEngine.Events;
using Spawners.Factories;
using Spawners.Point.Setters;
using Spawners.Point.Getters;
using SerializeInterface.Runtime;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.5.0
namespace Spawners.Core
{
    [Serializable]
    public sealed class Spawner<TComponent, TFactory> : ISpawnable<TComponent> 
        where TComponent : Component
        where TFactory : class, IFactory<TComponent>
    {
        private enum DebugMode
        {
            Off,
            On,
        }
        
        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private DebugMode _debugMode;
        
        [Space]
        [SerializeField] private TFactory _factory;
        
        [Space]
        [SerializeReference, Selector] private IGettablePoint _gettable;
        [SerializeReference, Selector] private ISettablePoint _settable;
        
        [Space, Header("Events")]
        [SerializeField] private UnityEvent<TComponent> _initialize = new();
        [SerializeField] private UnityEvent<TComponent> _spawned = new();
        #endregion

        #region Events
        public event UnityAction<TComponent> Initialize
        {
            add => _initialize.AddListener(value);
            remove => _initialize.RemoveListener(value);
        }
        
        public event UnityAction<TComponent> Spawned
        {
            add => _spawned.AddListener(value);
            remove => _spawned.RemoveListener(value);
        }
        #endregion

        private Spawner(TFactory factory, IGettablePoint gettable, 
            ISettablePoint settable, UnityAction<TComponent> initialize, DebugMode debugMode)
        {
            _factory = factory;
            _gettable = gettable;
            _settable = settable;
            
            _initialize.AddListener(initialize);
            _debugMode = debugMode;
        }

        public void Spawn()
        {
            var createdObj = _factory.Create((component) => _initialize.Invoke(component));
            if (createdObj == null)
            {
                if (_debugMode == DebugMode.On) Debug.LogWarning("There is no object to create");
                return;
            }
            
            if (_debugMode == DebugMode.On) Debug.LogWarning($"The object ({createdObj.name}) has been created");
            _settable.SetPoint(createdObj.transform, _gettable);
            _spawned?.Invoke(createdObj);
        }
        
        #region Create spawner functions
        public static Spawner<TComponent, TFactory> CreateSpawner(TFactory factory, IGettablePoint gettable,
            ISettablePoint settable, UnityAction<TComponent> initialize) =>
            new(factory, gettable, settable, initialize, DebugMode.Off);
        
        public static Spawner<TComponent, TFactory> CreateSpawnerWithDebug(TFactory factory, IGettablePoint gettable,
            ISettablePoint settable, UnityAction<TComponent> initialize) =>
            new(factory, gettable, settable, initialize, DebugMode.On);
        #endregion
    }
}
