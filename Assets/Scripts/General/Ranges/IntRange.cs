using System;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 2.0.0
namespace General.Ranges
{
    [Serializable]
    public struct IntRange
    {
        #region Inspector fields
        [SerializeField] private int _min;
        [SerializeField] private int _max;
        #endregion

        #region Properties
        public int Min => _min;

        public int Max => _max;
        #endregion

        public IntRange(int min, int max)
        {
            if (min > max)
                throw new ArgumentException($"Min can't be more than Max; Min = {min} Max = {max}");
            
            _min = min;
            _max = max;
        }

        public void AlignValueToRange(ref int value)
        {
            value = GetAlignValueToRange(value);
        }
        
        public int GetAlignValueToRange(int value)
        {
            if (value < Min) return Min;
            return value > Max ? Max : value;
        }
        
        public bool IsInRange(int value) => value >= Min && value <= Max;

        #region Create functions
        public static IntRange CreateMaxRange() => new(int.MinValue, int.MaxValue);
        
        public static IntRange CreateMaxPlusRange() => new(1, int.MaxValue);
        
        public static IntRange CreateMaxPlusWithZeroRange() => new(0, int.MaxValue);
        
        public static IntRange CreateMaxMinusRange() => new(int.MinValue, -1);
        
        public static IntRange CreateMaxMinusWithZeroRange() => new(int.MinValue, 0);
        #endregion
    }
}
