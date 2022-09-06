using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Editor
{
    public class ClassSelectorPopup : AdvancedDropdown
    {
        private Type[] _types;
        
        public event Action<ItemPopup> Selected;
        
        public ClassSelectorPopup(IEnumerable<Type> types, AdvancedDropdownState state) : base(state)
        {
            var enumerable = types as Type[] ?? types.ToArray();
            
            SetTypes(enumerable);
            minimumSize = new Vector2(minimumSize.x, EditorGUIUtility.singleLineHeight * (enumerable.ToArray().Length + 2));
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Select Type");
            Add(root, _types);
            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);
            if (item is ItemPopup itemPopup)
                Selected?.Invoke(itemPopup);
        }

        private void SetTypes(IEnumerable<Type> types) => _types = types.ToArray();

        #region Static functions
        private static void Add(AdvancedDropdownItem root, IEnumerable<Type> types)
        {
            var itemCount = 0;
            
            var nullItem = new ItemPopup(PopupMenuUtility.NullDisplayName,null)
            {
                id = itemCount++
            };
            root.AddChild(nullItem);
            
            var typeArray = types.OrderByType().ToArray();

            foreach (var type in typeArray) 
            {
                var splitTypePath = PopupMenuUtility.GetSplitTypePath(type);
                if (splitTypePath.Length == 0) continue;
            
                var parent = root;
                    
                for (var i = 0; splitTypePath.Length - 1 > i; i++) 
                {
                    var foundItem = GetItem(parent,splitTypePath[i]);
                    
                    if (foundItem != null) parent = foundItem;
                    else 
                    {
                        var newItem = new AdvancedDropdownItem(splitTypePath[i]) 
                        {
                            id = itemCount++,
                        };
                            
                        parent.AddChild(newItem);
                        parent = newItem;
                    }
                }
                
                var item = new ItemPopup(ObjectNames.NicifyVariableName(splitTypePath[^1]), type) 
                {
                    id = itemCount++
                };
                parent.AddChild(item);
            }
        }
        
        private static AdvancedDropdownItem GetItem(AdvancedDropdownItem parent, string name) =>
            parent.children.FirstOrDefault(item => item.name == name);
        #endregion

        public class ItemPopup : AdvancedDropdownItem
        {
            public Type Type { get; }
            
            public ItemPopup(string name, Type type) : base(name)
            {
                Type = type;
            }
        }
    }
}
