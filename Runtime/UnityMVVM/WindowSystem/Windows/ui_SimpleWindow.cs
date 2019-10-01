using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Toastapp.MVVM
{
	public class ui_SimpleWindow : MonoBehaviour
	{
		private Transform[] ObjectsToToggle;

		public List<ui_Coroutine> Show_PreProcessors = new List<ui_Coroutine>();
		public List<ui_Coroutine> Show_PostProcessors = new List<ui_Coroutine>();
		public List<ui_Coroutine> Hide_PreProcessors = new List<ui_Coroutine>();
		public List<ui_Coroutine> Hide_PostProcessors = new List<ui_Coroutine>();

		private void Awake()
		{
			ObjectsToToggle = GetComponentsInChildren<Transform>();
		}

		protected void OnEnable()
		{
			Open();
		}

		public void Open()
		{
			StartCoroutine(OpenProcess());
		}

		public void Close()
		{
			StartCoroutine(CloseProcess());
		}

		IEnumerator OpenProcess()
		{
			foreach(ui_Coroutine coroutine in Show_PreProcessors)
			{
				yield return coroutine.StartCoroutine(coroutine.Run());
			}

			foreach(Transform go in ObjectsToToggle)
			{
				go.gameObject.SetActive(true);
			}

			foreach(ui_Coroutine coroutine in Show_PostProcessors)
			{
				yield return coroutine.StartCoroutine(coroutine.Run());
			}
		}

		IEnumerator CloseProcess()
		{
			foreach(ui_Coroutine coroutine in Hide_PreProcessors)
			{
				yield return coroutine.StartCoroutine(coroutine.Run());
			}

			foreach(Transform go in ObjectsToToggle)
			{
				go.gameObject.SetActive(false);
			}

			foreach(ui_Coroutine coroutine in Hide_PostProcessors)
			{
				yield return coroutine.StartCoroutine(coroutine.Run());
			}

			this.gameObject.SetActive(false);
		}
	}
}