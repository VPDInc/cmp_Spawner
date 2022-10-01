using System;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Ranges
{
    [Serializable]
    public struct FloatRange
    {
        #region Inspector fields
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        #endregion

        #region Properties
        public float Min => _min;

        public float Max => _max;
        #endregion

        public FloatRange(float min, float max)
        {
            if (min > max)
                throw new ArgumentException($"Min can't be more than Max; Min = {min} Max = {max}");

            _min = min;
            _max = max;
        }

        public void AlignValueToRange(ref float value)
        {
            value = GetAlignValueToRange(value);
        }

        public float GetAlignValueToRange(float value)
        {
            if (value < Min) return Min;
            return value > Max ? Max : value;
        }

        public bool IsInRange(float value) => value >= Min && value <= Max;
        
        #region Create functions
        public static FloatRange CreateMaxRange() => new(float.MinValue, float.MaxValue);
        
        public static FloatRange CreateMaxPlusRange() => new(1, float.MaxValue);
        
        public static FloatRange CreateMaxPlusWithZeroRange() => new(0, float.MaxValue);
        
        public static FloatRange CreateMaxMinusRange() => new(float.MinValue, -1);
        
        public static FloatRange CreateMaxMinusWithZeroRange() => new(float.MinValue, 0);
        #endregion
    }
}
