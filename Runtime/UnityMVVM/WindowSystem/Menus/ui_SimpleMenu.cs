using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Toastapp.MVVM;
#endif

namespace Toastapp.MVVM
{
	public class ui_SimpleMenu : MonoBehaviour
	{
		private int currentWindowIndex;
		private ui_SimpleWindow currentWindow;
		public List<ui_SimpleWindow> MenuWindows;

		private void Awake()
		{
			foreach(var menuWindow in MenuWindows)
			{
				menuWindow.gameObject.SetActive(false);
			}
			MenuWindows[0].gameObject.SetActive(true);
		}

		public void Next()
		{
			if(currentWindowIndex < MenuWindows.Count - 1)
			{
				currentWindowIndex++;

				if(currentWindow != null)
				{
					currentWindow.Close();
				}

				currentWindow = MenuWindows[currentWindowIndex];

				if(currentWindow != null)
				{
					currentWindow.gameObject.SetActive(true);
					currentWindow.Open();
				}
			}
		}

		public void Previous()
		{
			if(currentWindowIndex > 0)
			{
				currentWindowIndex--;

				if(currentWindow != null)
				{
					currentWindow.Close();
				}

				currentWindow = MenuWindows[currentWindowIndex];

				if(currentWindow != null)
				{
					currentWindow.gameObject.SetActive(true);
					currentWindow.Open();
				}
			}
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(ui_SimpleMenu))]
public class ui_SimpleMenuEditor : Editor
{
	public override void OnInspectorGUI()
	{
		if(GUILayout.Button("Refresh"))
		{
			ui_SimpleMenu menu = ((ui_SimpleMenu)target);
			menu.MenuWindows = new List<ui_SimpleWindow>(menu.gameObject.GetComponentsInChildren<ui_SimpleWindow>(true));
		}

		base.OnInspectorGUI();
	}
}
#endif