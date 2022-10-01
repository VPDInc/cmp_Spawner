using System;
using UnityEngine;
using General.Ranges;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Parameters
{
    [Serializable]
    public struct IntParameter
    {
        #region Inspector fields
        [SerializeField] private IntRange _range;
        [SerializeField] private int _value;
        #endregion

        #region Properties
        public IntRange Range => _range;

        public int Value
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
        public IntParameter(int min, int max, int value) : this(new IntRange(min, max), value) { }

        public IntParameter(IntRange range, int value)
        {
            _range = range;
            if (_range.IsInRange(value))
                throw new ArgumentOutOfRangeException($"Value is in the wrong range; Min = {_range.Min}; Max = {_range.Max}; Value = {value}");
            
            _value = value;
        }
        #endregion
    }
}
