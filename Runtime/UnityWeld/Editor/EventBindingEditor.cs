using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityWeld.Binding;
using UnityWeld.Binding.Internal;

namespace UnityWeld_Editor
{
	[CustomEditor(typeof(EventBinding))]
	public class EventBindingEditor : BaseBindingEditor
	{
		private EventBinding targetScript;

		// Whether or not the values on our target match its prefab.
		private bool viewEventPrefabModified;
		private bool viewModelMethodPrefabModified;

		[SerializeField]
		private string parametersRawInfos;

		private void OnEnable()
		{
			targetScript = (EventBinding)target;
		}

		public override void OnInspectorGUI()
		{
			if(CannotModifyInPlayMode())
			{
				GUI.enabled = false;
			}

			UpdatePrefabModifiedProperties();

			var defaultLabelStyle = EditorStyles.label.fontStyle;
			EditorStyles.label.fontStyle = viewEventPrefabModified
				? FontStyle.Bold
				: defaultLabelStyle;

			ShowEventMenu(
				UnityEventWatcher.GetBindableEvents(targetScript.gameObject)
					.OrderBy(evt => evt.Name)
					.ToArray(),
				updatedValue => targetScript.ViewEventName = updatedValue,
				targetScript.ViewEventName
			);

			EditorStyles.label.fontStyle = viewModelMethodPrefabModified
				? FontStyle.Bold
				: defaultLabelStyle;

			ShowMethodMenu(targetScript, TypeResolver.FindBindableMethods(targetScript));

			if(targetScript.ViewModelMethodName != null)
			{
				var methodName = targetScript.ViewModelMethodName.Split('.');
				if(methodName != null && methodName.Length > 1)
				{
					var parametersInfos = TypeResolver.FindBindableMethodsParameters(targetScript, methodName[1]);
					if(parametersInfos != null)
					{
						targetScript.Parameter = EditorGUILayout.TextField($"[string only] {parametersInfos.Name} : ", targetScript.Parameter);
						UpdateProperty(updatedValue => targetScript.Parameter = updatedValue, targetScript.Parameter, targetScript.Parameter, "aezezaeza");
					}
				}
			}
	

			EditorStyles.label.fontStyle = defaultLabelStyle;
		}

		/// <summary>
		/// Draws the dropdown for selecting a method from bindableViewModelMethods
		/// </summary>
		private void ShowMethodMenu(
			EventBinding targetScript,
			BindableMember<MethodInfo>[] bindableMethods
		)
		{
			var tooltip = "Method on the view-model to bind to.";

			InspectorUtils.DoPopup(
				new GUIContent(targetScript.ViewModelMethodName),
				new GUIContent("View-model method", tooltip),
				m => m.ViewModelType + "/" + m.MemberName,
				m => true,
				m => m.ToString() == targetScript.ViewModelMethodName,
				m => UpdateProperty(
					updatedValue => targetScript.ViewModelMethodName = updatedValue,
					targetScript.ViewModelMethodName,
					m.ToString(),
					"Set bound view-model method"
				),
				bindableMethods
					.OrderBy(m => m.ViewModelTypeName)
					.ThenBy(m => m.MemberName)
					.ToArray()
			);
		}

		/// <summary>
		/// Check whether each of the properties on the object have been changed 
		/// from the value in the prefab.
		/// </summary>
		private void UpdatePrefabModifiedProperties()
		{
			var property = serializedObject.GetIterator();
			// Need to call Next(true) to get the first child. Once we have it, 
			// Next(false) will iterate through the properties.
			property.Next(true);
			do
			{
				switch(property.name)
				{
					case "viewEventName":
						viewEventPrefabModified = property.prefabOverride;
						break;

					case "viewModelMethodName":
						viewModelMethodPrefabModified = property.prefabOverride;
						break;
				}
			}
			while(property.Next(false));
		}
	}
}
