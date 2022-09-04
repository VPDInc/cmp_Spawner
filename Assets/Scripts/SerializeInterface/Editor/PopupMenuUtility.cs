using System;
using System.Linq;
using System.Collections.Generic;
using SerializeInterface.Runtime;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Editor
{
    public static class PopupMenuUtility
    {
        public const string NullDisplayName = "Null";

        public static IEnumerable<Type> OrderByType(this IEnumerable<Type> source) => 
            source.OrderBy(type => type == null ? -999 : 
            GetAttribute(type)?.Order ?? 0).
            ThenBy(type => type == null ? null : 
            GetAttribute(type)?.MenuName ?? type.Name);
        
        public static string[] GetSplitTypePath(Type type) 
        {
            var popupMenu = GetAttribute(type);
            if (popupMenu != null) return popupMenu.GetSplitMenuName();
            
            var splitIndex = type.FullName!.LastIndexOf('.');
            return splitIndex < 0 ? new[] { type.Name } :
                new[] { type.FullName[..splitIndex], type.FullName[(splitIndex + 1)..] };
        }

        public static PopupMenuAttribute GetAttribute(Type type) => 
            (PopupMenuAttribute)Attribute.GetCustomAttribute(type, typeof(PopupMenuAttribute));
    }
}
