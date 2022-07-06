using Pool;
using System;
using UnityEngine;
using UnityEngine.Pool;
using Spawners.Sequence;

using Object = UnityEngine.Object;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Factories.Generic.Core
{
    [Serializable]
    public sealed class PoolCreator<TComponent> : IFactory<TComponent>  where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] [Min(0)] private int _startCount;
        [SerializeField] [Min(1)] private int _maxCount;
        
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private TComponent[] _objs;
        #endregion
        
        #region Fields
        private ObjectPool<TComponent> _pool;
        private ElementGetterBySequence<TComponent> _elementGetterBySequence;
        #endregion

        public PoolCreator(int startCount, int maxCount,
            SequenceType sequenceType, params TComponent[] objs)
        {
            _startCount = startCount;
            _maxCount = maxCount;
            
            _sequenceType = sequenceType;
            _objs = objs;
        }
        
        public TComponent Create()
        {
            if (_pool == null) InitializePool();
            return _pool.Get();
        }
        
        public void PutToPool(TComponent obj) => _pool.Release(obj);

        #region Initialize functions
        private void InitializePool()
        {
            _pool = new ObjectPool<TComponent>(InstantiateObj, EnableObject, 
                DisableObject, DestroyObject, true, _startCount, _maxCount);
        }
        
        private TComponent InstantiateObj()
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<TComponent>(_sequenceType, _objs);
            
            var component = Object.Instantiate(_elementGetterBySequence.Get());
            if (component is IPooledObject<TComponent> pooledObject)
                pooledObject.ReturnedToPool += PutToPool;
            PutToPool(component);
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
