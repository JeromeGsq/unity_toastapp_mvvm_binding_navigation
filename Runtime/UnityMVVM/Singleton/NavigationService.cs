using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toastapp.MVVM
{
    public class NavigationService : GlobalSingleton<NavigationService>
    {
        private const string ViewPrefabsPath = "Prefabs/UI/Views";

        [SerializeField]
        private Transform RootView;

        [SerializeField]
        private List<GameObject> viewPrefabs;

        private List<Tuple<GameObject, Type>> viewAndViewModelTypePrefabs;

        private List<Tuple<GameObject, Type>> navigationStack = new List<Tuple<GameObject, Type>>();

        protected override void Awake()
        {
            base.Awake();
            this.ClearNavigationStack();
            this.LoadPrefabsFromResources();
            this.ExtractViewModels();
        }

        private void LoadPrefabsFromResources()
        {
            this.viewPrefabs = new List<GameObject>(Resources.LoadAll<GameObject>(ViewPrefabsPath));
        }

        private void ExtractViewModels()
        {
            this.viewAndViewModelTypePrefabs = new List<Tuple<GameObject, Type>>();

            foreach (var viewGameObject in viewPrefabs)
            {
                var components = viewGameObject.GetComponents(typeof(Component));

                foreach (var component in components)
                {
                    var interfaces = component.GetType().GetInterfaces();
                    foreach (var inter in interfaces)
                    {
                        // If this gameobject contains a component of type IViewModel...
                        if (inter.Equals(typeof(IViewModel)))
                        {
                            // ... add it to available Views
                            var viewModelComponent = component as IViewModel;
                            this.viewAndViewModelTypePrefabs.Add(Tuple.New(viewGameObject, viewModelComponent.GetType()));
                            break;
                        }
                    }
                }
            }
        }

        public GameObject ShowViewModel(Type destinationViewModelType)
        {
            return this.ShowViewModel<object>(destinationViewModelType, null);
        }

        public GameObject ShowViewModel(Type destinationViewModelType, Transform root)
        {
            return this.ShowViewModel<object>(destinationViewModelType, null, root);
        }

        public GameObject ShowViewModel<T>(Type destinationViewModelType, T parameters, Transform rootView = null)
        {
            // Use default root
            if (rootView == null)
            {
                rootView = this.RootView;
            }

            // Current viewmodel should be set as background
            if (this.navigationStack != null && this.navigationStack?.Count > 0)
            {
                foreach (var component in this.navigationStack?.LastOrDefault()?.GameObject?.GetComponents(typeof(Component)))
                {
                    var interfaces = component.GetType().GetInterfaces();
                    foreach (var inter in interfaces)
                    {
                        if (inter.Equals(typeof(IViewModel)))
                        {
                            var viewModel = component as IViewModel;
                            viewModel.SetInBackground(true);
                            break;
                        }
                    }
                }
            }


            foreach (var viewAndViewModelTypeGameObject in this.viewAndViewModelTypePrefabs)
            {
                // If this is the correct destinationViewModelType...
                if (viewAndViewModelTypeGameObject.Type.Equals(destinationViewModelType))
                {
                    // ... create View ...
                    GameObject instantiatedView = Instantiate(viewAndViewModelTypeGameObject.GameObject, rootView);
                    var components = instantiatedView.GetComponents(typeof(Component));

                    // ... and initialize its ViewModel
                    foreach (var component in components)
                    {
                        var interfaces = component.GetType().GetInterfaces();
                        foreach (var inter in interfaces)
                        {
                            if (inter.Equals(typeof(IViewModel)))
                            {
                                var viewModel = component as IViewModel;
                                viewModel.SetParameters(parameters);
                                break;
                            }
                        }
                    }

                    this.navigationStack.Add(Tuple.New(instantiatedView, viewAndViewModelTypeGameObject.Type));
                    return instantiatedView;
                }
            }

            return null;
        }

        public void CloseViewModel(IViewModel viewModelInstance)
        {
            Tuple<GameObject, Type> selectedInstance = null;

            foreach (var item in this.navigationStack)
            {
                if (viewModelInstance.GetType().Equals(item.Type))
                {
                    selectedInstance = item;
                }
            }

            Destroy(selectedInstance?.GameObject);
            this.navigationStack.Remove(selectedInstance);

            var components = this.navigationStack.LastOrDefault()?.GameObject?.GetComponents(typeof(Component));
            if (components != null)
            {
                foreach (var component in components)
                {
                    var interfaces = component.GetType().GetInterfaces();
                    foreach (var inter in interfaces)
                    {
                        if (inter.Equals(typeof(IViewModel)))
                        {
                            var viewModel = component as IViewModel;
                            viewModel.SetInBackground(false);
                            break;
                        }
                    }
                }
            }
        }

        public void ClearNavigationStack()
        {
            foreach (Transform child in this.RootView.transform)
            {
                Destroy(child.gameObject);
            }
            this.navigationStack?.Clear();

        }
    }
}