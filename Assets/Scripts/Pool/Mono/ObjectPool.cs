using System;
using UnityEngine;
using General.Sequence;
using UnityEngine.Events;

namespace Pool.Mono
{
    public class ObjectPool : ObjectPool<Component> { }
    
    public class ObjectPool<TComponent> : MonoBehaviour where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] private bool _onInitializeAwake;
        
        [SerializeField] [Min(0)] private int _startCount;
        [SerializeField] [Min(0)] private int _defaultCapacity;
        [SerializeField] [Min(1)] private int _maxSize;
        
        [SerializeField] private Transform _content;
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private TComponent[] _objs;
        #endregion
        
        #region Fields
        private Core.ObjectPool<TComponent> _pool;
        private ElementGetterBySequence<TComponent> _elementGetterBySequence;
        private UnityAction<TComponent> _initialize;
        #endregion

        public bool IsInitialize => _pool != null;

        private void Awake()
        {
            if (_onInitializeAwake) Initialize();
        }

        public TComponent Get(UnityAction<TComponent> initialize)
        {
            _initialize = initialize;
            if (_pool == null) Initialize();
            
            var component = _pool.Get();
            return component;
        }

        public void Release(TComponent obj) => _pool.Release(obj);

        #region Initialize functions
        public void Initialize()
        {
            if (_pool != null) throw new Exception("Pool can't be created twice");
            
            _pool = new Core.ObjectPool<TComponent>(InstantiateObj, EnableObject, 
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
            
            var component = Instantiate(_elementGetterBySequence.Get(), _content, true);
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

        private static void DestroyObject(TComponent component) => Destroy(component.gameObject);
    }
}
