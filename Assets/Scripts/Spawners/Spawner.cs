using UnityEngine;
using UnityEngine.Events;
using Spawners.Factories;
using Spawners.Point.Getters;
using Spawners.Point.Setters;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners
{
    public abstract class Spawner<TComponent> : MonoBehaviour, ISpawnable<TComponent> where TComponent : Component
    {
        #region Inspector fields
        [Header("Settings")]
        [SerializeField] private bool _isDebug;

        [Header("Events")]
        [SerializeField] private UnityEvent<TComponent> _spawned = new();
        #endregion

        public event UnityAction<TComponent> Spawned
        {
            add => _spawned.AddListener(value);
            remove => _spawned.RemoveListener(value);
        }
        
        #region Properties
        protected abstract IFactory<TComponent> Factory { get; }
        protected abstract IGettablePoint Gettable { get; }
        protected abstract ISettablePoint Settable { get; }
        protected abstract UnityAction<TComponent> Initialize { get; }
        #endregion

        public void Spawn()
        {
            var createdObj = Factory.Create(Initialize);
            if (createdObj == null)
            {
                if (_isDebug) Debug.LogWarning("There is no object to create");
                return;
            }
            
            Settable.SetPoint(createdObj.transform, Gettable);
            _spawned?.Invoke(createdObj);
        }
    }
}
