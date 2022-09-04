#if UNITY_EDITOR
using System;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface, Inherited = false)]
    public class PopupMenuAttribute : Attribute
    {
        private static readonly char[] Separator = { '/', '\\' };

        #region Properties
        public int Order { get; }
        public string MenuName { get; }
        #endregion

        public PopupMenuAttribute(string menuName, int order = 0)
        {
            Order = order;
            MenuName = menuName;
        }

        #region Get functions
        public string GetMenuNameWithoutPath()
        {
            var splitMenuName = GetSplitMenuName();
            return splitMenuName.Length != 0 ? splitMenuName[^1] : null;
        }

        public string[] GetSplitMenuName() => !string.IsNullOrWhiteSpace(MenuName) ?
            MenuName.Split(Separator, StringSplitOptions.RemoveEmptyEntries) :
            Array.Empty<string>();
        #endregion
    }
}
#endif
