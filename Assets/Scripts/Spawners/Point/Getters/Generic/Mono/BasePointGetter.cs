using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.2
namespace Spawners.Point.Getters.Generic.Mono
{
    public abstract class BasePointGetter : MonoBehaviour, IGettablePoint
    {
        public abstract Point GetPoint();
    }
}
