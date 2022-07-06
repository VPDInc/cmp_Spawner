using UnityEngine;
using UnityEngine.Events;
using Spawners.Factories;
using Spawners.Point.Getters;
using Spawners.Point.Setters;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners
{
    public abstract class Spawner<TComponent> : MonoBehaviour, ISpawnable where TComponent : Component
    {
        [Header("Settings")]
        [SerializeField] private bool _isDebug;
        
        #region Properties
        protected abstract IFactory<TComponent> Factory { get; }
        protected abstract IGettablePoint Gettable { get; }
        protected abstract ISettablePoint Settable { get; }
        protected abstract UnityAction<TComponent> Initialize { get; }
        #endregion

        public void Spawn()
        {
            var createdObj = Factory.Create();
            if (createdObj == null)
            {
                if (_isDebug) Debug.LogWarning("There is no object to create");
                return;
            }
            
            Settable.SetPoint(createdObj.transform, Gettable);
            Initialize?.Invoke(createdObj);
        }
    }
}
