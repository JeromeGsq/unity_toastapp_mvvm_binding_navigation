using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityWeld.Binding.Internal;

namespace UnityWeld.Binding
{
    /// <summary>
    /// Class for binding Unity UI events to methods in a view model.
    /// </summary>
    [AddComponentMenu("Unity Weld/Event Binding")]
    [HelpURL("https://github.com/Real-Serious-Games/Unity-Weld")]
    public class EventBinding : AbstractMemberBinding
    {
        /// <summary>
        /// Name of the method in the view model to bind to.
        /// </summary>
        public string ViewModelMethodName
        {
            get { return viewModelMethodName; }
            set { viewModelMethodName = value; }
        }

        [SerializeField]
        private string viewModelMethodName;

        /// <summary>
        /// Name of the event in the view to bind to.
        /// </summary>
        public string ViewEventName
        {
            get { return viewEventName; }
            set { viewEventName = value; }
        }

        [SerializeField, FormerlySerializedAs("uiEventName")]
        private string viewEventName;

		public string Parameter
        {
            get { return parameter; }
            set { parameter = value;
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }

        [SerializeField]
		private string parameter;

        /// <summary>
        /// Watches a Unity event for updates.
        /// </summary>
        private UnityEventWatcher eventWatcher;

        public override void Connect()
        {
            string methodName;
            object viewModel;
            ParseViewModelEndPointReference(
                viewModelMethodName, 
                out methodName, 
                out viewModel
            );

			var viewModelMethod = viewModel.GetType().GetMethod(methodName);
			var parametersInfos = viewModel.GetType().GetMethod(methodName).GetParameters();

			var parameters = new List<string>(){};
			for(int i = 0; i < parametersInfos.Length; i++)
			{
				parameters.Add(parameter);
			}

			string eventName;
            Component view;
            ParseViewEndPointReference(viewEventName, out eventName, out view);

            eventWatcher = new UnityEventWatcher(view, eventName, 
                () =>
                {
                    if (viewModelMethod != null)
                    {
                        viewModelMethod.Invoke(viewModel, parameters.ToArray());
                    }
                });
        }

        public override void Disconnect()
        {
            if (eventWatcher != null)
            {
                eventWatcher.Dispose();
                eventWatcher = null;
            }
        }
    }
}
