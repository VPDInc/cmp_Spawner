#if UNITY_EDITOR
using System;
using UnityEngine;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SelectorAttribute : PropertyAttribute { }
}
#endif
