using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Point
{
    public struct Point
    {
        #region Fields
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;
        #endregion

        #region Constructor
        public Point(Transform transform)
        {
            Position = transform.position;
            Rotation = transform.rotation;
        }
        
        public Point(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }
        #endregion
    }
}
