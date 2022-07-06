using System;
using UnityEngine;

using Random = UnityEngine.Random;

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
        
        public Quaternion Get()
        {
            var x = Random.Range(_minLimit.x, _maxLimit.x);
            var y = Random.Range(_minLimit.y, _maxLimit.y);
            var z = Random.Range(_minLimit.z, _maxLimit.z);
            return Quaternion.Euler(new Vector3(x, y, z));
        }
    }
}
