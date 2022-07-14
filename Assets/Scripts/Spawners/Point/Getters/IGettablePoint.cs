using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Point.Getters
{
    public interface IGettablePoint
    {
        public Point GetPoint() => new(Vector3.zero, Quaternion.identity);
    }
}
