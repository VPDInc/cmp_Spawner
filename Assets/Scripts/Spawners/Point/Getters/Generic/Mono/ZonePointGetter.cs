using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.3
namespace Spawners.Point.Getters.Generic.Mono
{
    public sealed class ZonePointGetter : BasePointGetter
    {
        [SerializeField] private Core.ZonePointGetter _getter;

        public override Point GetPoint() => _getter.GetPoint();
    }
}
