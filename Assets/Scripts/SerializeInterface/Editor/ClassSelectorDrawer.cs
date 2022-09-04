using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using SerializeInterface.Runtime;
using System.Collections.Generic;

// Code by VPDInc
// Email: vpd-2000@yandex.com
// Version: 1.0.0
namespace SerializeInterface.Editor
{
    [CustomPropertyDrawer(typeof(SelectorAttribute))]
    public class ClassSelectorDrawer : PropertyDrawer
    {
	    #region Readonly fields
	    private static readonly Type _unityObjectType = typeof(UnityEngine.Object);
		
		private static readonly GUIContent _nullDisplayName = new(PopupMenuUtility.NullDisplayName);
		private static readonly GUIContent _isNotManagedReferenceLabel = new("The property type is not manage reference.");

		private readonly Dictionary<string,TypePopupCache> _typePopups = new();
		private readonly Dictionary<string,GUIContent> _typeNameCaches = new ();
		#endregion

		private SerializedProperty _targetProperty;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
		{
			EditorGUI.BeginProperty(position, label, property);

			if (property.propertyType == SerializedPropertyType.ManagedReference) 
			{
				var popupPosition = new Rect(position);
				popupPosition.width -= EditorGUIUtility.labelWidth;
				popupPosition.x += EditorGUIUtility.labelWidth;
				popupPosition.height = EditorGUIUtility.singleLineHeight;

				if (EditorGUI.DropdownButton(popupPosition,GetTypeName(property), FocusType.Keyboard)) 
				{
					var popup = GetTypePopup(property);
					_targetProperty = property;
					popup.TypePopup.Show(popupPosition);
				}
				
				EditorGUI.PropertyField(position, property, label,true);
			} 
			else EditorGUI.LabelField(position,label,_isNotManagedReferenceLabel);

			EditorGUI.EndProperty();
		}
		
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
			EditorGUI.GetPropertyHeight(property,true);

		private TypePopupCache GetTypePopup(SerializedProperty property) 
		{
			var managedReferenceFieldTypeName = property.managedReferenceFieldTypename;
			if (_typePopups.TryGetValue(managedReferenceFieldTypeName, out var result)) return result;
			
			var state = new AdvancedDropdownState();
			var baseType = ManagedReferenceUtility.GetType(managedReferenceFieldTypeName);
				
			var popup = new ClassSelectorPopup(
				TypeCache.GetTypesDerivedFrom(baseType).Append(baseType).Where(p =>
					(p.IsPublic || p.IsNestedPublic) &&
					!p.IsAbstract && !p.IsGenericType &&
					!_unityObjectType.IsAssignableFrom(p) &&
					Attribute.IsDefined(p,typeof(SerializableAttribute))
				),
				state
			);
				
			popup.Selected += item => 
			{
				var type = item.Type;
				var obj = _targetProperty.SetManagedReference(type);
					
				_targetProperty.isExpanded = obj != null;
				_targetProperty.serializedObject.ApplyModifiedProperties();
				_targetProperty.serializedObject.Update();
			};

			result = new TypePopupCache(popup);
			_typePopups.Add(managedReferenceFieldTypeName, result);
			return result;
		}

		private GUIContent GetTypeName(SerializedProperty property) 
		{
			var managedReferenceFullTypename = property.managedReferenceFullTypename;

			if (string.IsNullOrEmpty(managedReferenceFullTypename)) return _nullDisplayName;
			if (_typeNameCaches.TryGetValue(managedReferenceFullTypename, out var cachedTypeName)) return cachedTypeName;

			string typeName = null;
			var type = ManagedReferenceUtility.GetType(managedReferenceFullTypename);
			var typeMenu = PopupMenuUtility.GetAttribute(type);
			
			if (typeMenu != null) 
			{
				typeName = typeMenu.GetMenuNameWithoutPath();
				if (!string.IsNullOrWhiteSpace(typeName)) 
					typeName = ObjectNames.NicifyVariableName(typeName);
			}

			if (string.IsNullOrWhiteSpace(typeName)) typeName = ObjectNames.NicifyVariableName(type.Name);

			var result = new GUIContent(typeName);
			_typeNameCaches.Add(managedReferenceFullTypename, result);
			return result;
		}

		private struct TypePopupCache 
		{
			public ClassSelectorPopup TypePopup { get; }

			public TypePopupCache (ClassSelectorPopup typePopup) 
			{
				TypePopup = typePopup;
			}
		}
    }
}
