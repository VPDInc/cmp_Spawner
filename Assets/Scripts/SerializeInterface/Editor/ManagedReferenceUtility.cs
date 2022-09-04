using System;
using UnityEditor;
using System.Reflection;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Editor
{
    public static class ManagedReferenceUtility
    {
        public static object SetManagedReference(this SerializedProperty property, Type type) 
        {
            var obj = type != null ? Activator.CreateInstance(type) : null;
            property.managedReferenceValue = obj;
            return obj;
        }

        public static Type GetType(string typeName) 
        {
            var splitIndex = typeName.IndexOf(' ');
            var assembly = Assembly.Load(typeName[..splitIndex]);
            return assembly.GetType(typeName[(splitIndex + 1)..]);
        }
    }
}
