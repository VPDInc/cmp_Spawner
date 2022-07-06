using System;
using UnityEngine;
using Spawners.Sequence;

using Object = UnityEngine.Object;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Factories.Generic.Core
{
    [Serializable]
    public sealed class InstantiateCreator<TComponent> : IFactory<TComponent> where TComponent : Component
    {
        #region Inspector fields
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private TComponent[] _objs;
        #endregion

        private ElementGetterBySequence<TComponent> _elementGetterBySequence;
        
        public InstantiateCreator(SequenceType sequenceType, params TComponent[] objs)
        {
            _sequenceType = sequenceType;
            _objs = objs;
        }

        public TComponent Create()
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<TComponent>(_sequenceType, _objs);
            return Object.Instantiate(_elementGetterBySequence.Get());
        }
    }
}
