using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1
namespace Spawners.Factories
{
    public interface IFactory<out TComponent> where TComponent : Component
    {
        public TComponent Create();
    }
}
