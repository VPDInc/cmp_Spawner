using Pool;
using System;
using UnityEngine;
using Spawners.Sequence;
using UnityEngine.Events;

using Object = UnityEngine.Object;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Factories.Generic.Core
{
    [Serializable]
    public sealed class PoolCreator<TComponent> : IFactory<TComponent>  where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] [Min(0)] private int _startCount;
        [SerializeField] [Min(0)] private int _defaultCapacity;
        [SerializeField] [Min(1)] private int _maxSize;
        
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private TComponent[] _objs;
        #endregion
        
        #region Fields
        private ObjectPool<TComponent> _pool;
        private ElementGetterBySequence<TComponent> _elementGetterBySequence;
        private UnityAction<TComponent> _initialize;
        #endregion

        public PoolCreator(int startCount, int defaultCapacity, int maxSize,
            SequenceType sequenceType, params TComponent[] objs)
        {
            _startCount = startCount;
            _defaultCapacity = defaultCapacity;
            _maxSize = maxSize;
            
            _sequenceType = sequenceType;
            _objs = objs;
        }
        
        public TComponent Create(UnityAction<TComponent> initialize)
        {
            _initialize = initialize;
            if (_pool == null) InitializePool();
            
            var component = _pool.Get();
            return component;
        }
        
        public void Release(TComponent obj) => _pool.Release(obj);

        #region Initialize functions
        private void InitializePool()
        {
            _pool = new ObjectPool<TComponent>(InstantiateObj, EnableObject, 
                DisableObject, DestroyObject, false, _defaultCapacity, _maxSize);

            for (var i = 0; i < _startCount; i++)
            {
                var component = InstantiateObj();
                Release(component);
            }
        }
        
        private TComponent InstantiateObj()
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<TComponent>(_sequenceType, _objs);
            
            var component = Object.Instantiate(_elementGetterBySequence.Get());
            if (component is IPooledObject<TComponent> pooledObject)
                pooledObject.ReturnedToPool += Release;
            
            _initialize?.Invoke(component);
            return component;
        }
        #endregion
         
        private static void EnableObject(TComponent component)
        {
            if (!component.gameObject.activeSelf) 
                component.gameObject.SetActive(true);
        }

        private static void DisableObject(TComponent component)
        {
            if (component.gameObject.activeSelf)
                component.gameObject.SetActive(false);
        }

        private static void DestroyObject(TComponent component) => Object.Destroy(component.gameObject);
    }
}
