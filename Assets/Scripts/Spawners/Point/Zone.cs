using System;
using UnityEngine;

using Random = UnityEngine.Random;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Point
{
    [Serializable]
    public struct Zone
    {
        #region Inspector fields
        [SerializeField] private Vector3 _minLimit;
        [SerializeField] private Vector3 _maxLimit;
        #endregion
        
        #region Properties
        public Vector3 MinLimit => _minLimit;
        public Vector3 MaxLimit => _maxLimit;
        #endregion

        #region Constructors
        public Zone(Vector3 minLimit, Vector3 maxLimit)
        {
            var (isException, exceptionMessage) = (false, "");
            
            if (minLimit.x > maxLimit.x)
            {
                isException = true;
                exceptionMessage += $"maxLimit.x {maxLimit.x} can't be less than minLimit.x {minLimit.x}";
            }
            
            if (minLimit.y > maxLimit.y)
            {
                isException = true;
                exceptionMessage += $"maxLimit.y {maxLimit.y} can't be less than minLimit.y {minLimit.y}";
            }
            
            if (minLimit.z > maxLimit.z)
            {
                isException = true;
                exceptionMessage += $"maxLimit.z {maxLimit.z} can't be less than minLimit.z {minLimit.z}";
            }

            if (isException) throw new ArgumentException(exceptionMessage);
            
            _minLimit = minLimit;
            _maxLimit = maxLimit;
        }
        
        public Vector3 GetPosition()
        {
            var x = Random.Range(_minLimit.x, _maxLimit.x);
            var y = Random.Range(_minLimit.y, _maxLimit.y);
            var z = Random.Range(_minLimit.z, _maxLimit.z);
            return new Vector3(x, y, z);
        }
        #endregion
    }
}
