using UnityEngine;
using Spawners.Point.Getters;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Point.Setters
{
    public interface ISettablePoint
    {
        public void SetPoint(Transform transform, IGettablePoint gettable)
        {
            var point = gettable.GetPoint();
            transform.position = point.Position;
            transform.rotation = point.Rotation;
        }
    }
}
