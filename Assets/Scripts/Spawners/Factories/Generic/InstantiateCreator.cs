using System;
using UnityEngine;
using General.Sequence;
using UnityEngine.Events;

using Object = UnityEngine.Object;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.5.0
namespace Spawners.Factories.Generic
{
    [Serializable]
    public sealed class InstantiateCreator<TComponent> : IFactory<TComponent> where TComponent : Component
    {
        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private Transform _content;
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private TComponent[] _objs;
        #endregion

        private ElementGetterBySequence<TComponent> _elementGetterBySequence;
        
        public InstantiateCreator(SequenceType sequenceType, params TComponent[] objs)
        {
            _sequenceType = sequenceType;
            _objs = objs;
        }

        public TComponent Create(UnityAction<TComponent> initialize)
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<TComponent>(_sequenceType, _objs);
            
            var component = Object.Instantiate(_elementGetterBySequence.Get(), _content, true);
            initialize?.Invoke(component);

            return component;
        }
    }
}
