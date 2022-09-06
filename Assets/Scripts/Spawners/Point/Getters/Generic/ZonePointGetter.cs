using System;
using General.Sequence;
using UnityEngine;
using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.5.0
namespace Spawners.Point.Getters.Generic
{
    [Serializable]
    public sealed class ZonePointGetter : IGettablePoint
    {
        public enum RotationMode
        {
            Random,
            Manual,
            Identity,
        }

        #region Inspector fields
        [Header("Parameters")]
        [SerializeField] private RotationMode _rotationMode;
        [SerializeField] private RotationLimit _rotationLimit;

        [Space]

        [Header("Zones")]
        [SerializeField] private SequenceType _sequenceType;
        [SerializeField] private Zone[] _zones;
        #endregion

        private ElementGetterBySequence<Zone> _elementGetterBySequence;

        public ZonePointGetter() { }
        
        public ZonePointGetter(RotationMode rotationMode, SequenceType sequenceType, 
            RotationLimit rotationLimit, params Zone[] zones)
        {
            if (zones == null || zones.Length == 0)
                throw new NullReferenceException("Zones are not initialized");
            
            _rotationMode = rotationMode;
            _sequenceType = sequenceType;
            _rotationLimit = rotationLimit;
            
            _zones = zones;
        }

        public Point GetPoint()
        {
            _elementGetterBySequence ??= new ElementGetterBySequence<Zone>(_sequenceType, _zones);
            var position = _elementGetterBySequence.Get().GetPosition();
            
            var rotation = _rotationMode switch
            {
                RotationMode.Random => Random.rotation,
                RotationMode.Manual => _rotationLimit.Get(),
                RotationMode.Identity => Quaternion.identity,
                _ => throw new ArgumentOutOfRangeException()
            };

            return new Point(position, rotation);
        }
    }
}
