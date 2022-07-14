using System;
using UnityEngine;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Point
{
    [Serializable]
    public struct RotationLimit
    {
        #region Inspector fields
        [SerializeField] private Vector3 _minLimit;
        [SerializeField] private Vector3 _maxLimit;
        #endregion

        public RotationLimit(Vector3 minLimit, Vector3 maxLimit)
        {
            IsException(minLimit, maxLimit);
            
            _minLimit = minLimit;
            _maxLimit = maxLimit;
        }
        
        public Quaternion Get()
        {
            var x = Random.Range(_minLimit.x, _maxLimit.x);
            var y = Random.Range(_minLimit.y, _maxLimit.y);
            var z = Random.Range(_minLimit.z, _maxLimit.z);
            return Quaternion.Euler(new Vector3(x, y, z));
        }

        private static void IsException(Vector3 minLimit, Vector3 maxLimit)
        {
            var message = "";

            if (minLimit.x > maxLimit.x)
                message += $"maxLimit.x {maxLimit.x} can't be less than minLimit.x {minLimit.x} ";
            
            if (minLimit.y > maxLimit.y)
                message += $"maxLimit.y {maxLimit.y} can't be less than minLimit.y {minLimit.y} ";
            
            if (minLimit.z > maxLimit.z)
                message += $"maxLimit.z {maxLimit.z} can't be less than minLimit.z {minLimit.z}";
            
            if (string.IsNullOrEmpty(message)) throw new ArgumentException(message);
        }
    }
}
