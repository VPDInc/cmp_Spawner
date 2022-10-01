using System;
using UnityEngine;
using General.Ranges;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Parameters
{
    [Serializable]
    public struct FloatParameter
    {
        #region Inspector fields
        [SerializeField] private FloatRange _range;
        [SerializeField] private float _value;
        #endregion
    
        #region Properties
        public FloatRange Range => _range;

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                _range.AlignValueToRange(ref _value);
            }
        }
        #endregion

        #region Constructor
        public FloatParameter(float min, float max, float value) : this(new FloatRange(min, max), value) { }

        public FloatParameter(FloatRange range, float value)
        {
            _range = range;
            if (_range.IsInRange(value))
                throw new ArgumentOutOfRangeException($"Value is in the wrong range; Min = {_range.Min}; Max = {_range.Max}; Value = {value}");
            
            _value = value;
        }
        #endregion
    }
}
