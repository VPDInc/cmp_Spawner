using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.5.0
namespace Spawners.Point.Getters
{
    public interface IGettablePoint
    {
        public Point GetPoint() => new(Vector3.zero, Quaternion.identity);
    }
}
