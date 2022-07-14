using System;
using UnityEngine;
using Spawners.Sequence;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Point.Getters.Generic.Core
{
    [Serializable]
    public sealed class TransformPointGetter : IGettablePoint
    {
        public enum RotationMode
        {
            Random,
            Manual,
            Identity,
            FromPoint,
        }

        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private RotationMode _rotationMode;
        [SerializeField] private RotationLimit _rotationLimit;

        [Space]
        
        [Header("Transforms")]
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private Transform[] _transforms;
        #endregion
        
        private ElementGetterBySequence<Transform> _elementGetterBySequence;

        public TransformPointGetter(RotationMode rotationMode, SequenceType sequenceType, 
            RotationLimit rotationLimit, params Transform[] transforms)
        {
            if (transforms == null || transforms.Length == 0)
                throw new NullReferenceException("Transforms are not initialized");
            
            _rotationMode = rotationMode;
            _sequenceType = sequenceType;
            _rotationLimit = rotationLimit;
            
            _transforms = transforms;
        }

        public Point GetPoint()
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<Transform>(_sequenceType, _transforms);
            var transform = _elementGetterBySequence.Get();

            var rotation = _rotationMode switch
            {
                RotationMode.Random => Random.rotation,
                RotationMode.Manual => _rotationLimit.Get(),
                RotationMode.Identity => Quaternion.identity,
                RotationMode.FromPoint => transform.rotation,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Point(transform.position, rotation);
        }
    }
}
