using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners.Point.Getters.Generic.Mono
{
    public sealed class TransformPointGetter : BasePointGetter
    {
        [SerializeField] private Core.TransformPointGetter _getter;

        public override Point GetPoint() => _getter.GetPoint();
    }
}
