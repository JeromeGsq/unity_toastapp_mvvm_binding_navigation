#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MVVMGenerator : EditorWindow
{
    private static string url = "Packages\\com.toastapp.mvvm_binding_navigation\\Editor\\MVVMGenerator\\";
    private static DefaultAsset scriptFolder;
    private string scriptName;

    [MenuItem("Tools/MVVM/MVVM Files Generator")]
    private static void ShowWindow()
    {
        MVVMGenerator window = CreateInstance(typeof(MVVMGenerator)) as MVVMGenerator;
        window.ShowUtility();
    }

    private void SelectFolder()
    {
        if (scriptFolder == null)
        {
            string asset = AssetDatabase.FindAssets("MVVMBindings").FirstOrDefault();
            scriptFolder = (DefaultAsset)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(asset), typeof(UnityEngine.Object));
        }
    }

    private void OnGUI()
    {
        this.SelectFolder();


        GUILayout.BeginVertical(GUILayout.Width(400), GUILayout.Height(400));

        GUILayout.Label("Classes name:");
        scriptName = EditorGUILayout.TextField(scriptName);
        GUILayout.Space(20);

        GUILayout.Label("Preview:");
        GUILayout.Label(AssetDatabase.GetAssetPath(scriptFolder) + "/ViewModels/" + scriptName + "/" + scriptName + "ViewModel.cs");
        GUILayout.Label(AssetDatabase.GetAssetPath(scriptFolder) + "/Views/" + scriptName + "/" + scriptName + "View.cs");
        GUILayout.Label("Assets/Resouces/Prefabs/UI/Views/" + scriptName + "View.prefab");

        GUILayout.Space(20);

        if (GUILayout.Button("1- Generate Folder Structure", GUILayout.Height(50)))
        {
            this.GenerateFolderStructure();
        }

        if (GUILayout.Button("2- Generate Scripts", GUILayout.Height(50)))
        {
            this.GenerateScripts();
        }

        if (GUILayout.Button("3- Configure Prefab", GUILayout.Height(50)))
        {
            ConfigurePrefab();
        }

        GUILayout.EndVertical();
    }

    #region Methods
    private void GenerateFolderStructure()
    {
        try
        {
            string[] urls =
            {
                "Project/Scripts/MVVMBindings/ViewModels/" + scriptName,
                "Project/Scripts/MVVMBindings/Views/" + scriptName,
                "Resources/Prefabs/UI/Views"
            };

#if UNITY_EDITOR_OSX
            for (int i = 0; i < urls.Length; i++)
            {
                urls[i] = urls[i].Replace("\\", "/");
            }
#endif
            foreach (var url in urls)
            {
                GenerateFolder(url);
            }

            Debug.Log("GenerateFolderStructure Complete");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void GenerateScripts()
    {
        try
        {
            var path = AssetDatabase.GetAssetPath(scriptFolder);
            var templateViewPath = path + "\\Views\\" + scriptName + "\\" + scriptName + "View.cs";
            var templateViewModelPath = path + "\\ViewModels\\" + scriptName + "\\" + scriptName + "ViewModel.cs";
            var rootPrefabPath = url + "Prefabs\\MVVMPrefab.prefab";
            var destinationPrefabPath = "Assets\\Resources\\Prefabs\\UI\\Views\\" + scriptName + "View.prefab";
#if UNITY_EDITOR_OSX
            url = url.Replace("\\", "/");
            templateViewPath = templateViewPath.Replace("\\", "/");
            templateViewModelPath = templateViewModelPath.Replace("\\", "/");
            rootPrefabPath = rootPrefabPath.Replace("\\", "/");
            destinationPrefabPath = destinationPrefabPath.Replace("\\", "/");
#endif

            var templateView = File.ReadAllText(url + "ViewTemplate.txt");
            templateView = templateView.Replace("-arg-", scriptName);
            File.WriteAllText(templateViewPath, templateView);

            var templateViewModel = File.ReadAllText(url + "ViewModelTemplate.txt");
            templateViewModel = templateViewModel.Replace("-arg-", scriptName);
            File.WriteAllText(templateViewModelPath, templateViewModel);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("GenerateScripts Complete");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private void ConfigurePrefab()
    {
        try
        {
            var path = AssetDatabase.GetAssetPath(scriptFolder);
            var templateViewPath = path + "\\Views\\" + scriptName + "\\" + scriptName + "View.cs";
            var templateViewModelPath = path + "\\ViewModels\\" + scriptName + "\\" + scriptName + "ViewModel.cs";
            var rootPrefabPath = url + "Prefabs\\MVVMPrefab.prefab";
            var destinationPrefabPath = "Assets\\Resources\\Prefabs\\UI\\Views\\" + scriptName + "View.prefab";
            var viewTypeName = $"{scriptName}View, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

#if UNITY_EDITOR_OSX
            url = url.Replace("\\", "/");
            templateViewPath = templateViewPath.Replace("\\", "/");
            templateViewModelPath = templateViewModelPath.Replace("\\", "/");
            rootPrefabPath = rootPrefabPath.Replace("\\", "/");
            destinationPrefabPath = destinationPrefabPath.Replace("\\", "/");
#endif
            Type viewType = Type.GetType(viewTypeName);
            Debug.Log(viewType);

            var prefabView = PrefabUtility.LoadPrefabContents(rootPrefabPath);
            prefabView.AddComponent(viewType);
            PrefabUtility.SaveAsPrefabAssetAndConnect(prefabView, destinationPrefabPath, InteractionMode.AutomatedAction);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("ConfigurePrefab Complete");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    private static void GenerateFolder(string link)
    {
        var root = "Assets";

        var folders = link.Split('/');

        if (folders.Length == 0)
        {
            folders = new string[] { link };
        }

        for (int i = 0; i < folders.Length; i++)
        {
            if (!AssetDatabase.IsValidFolder(root + "/" + folders[i]))
            {
                AssetDatabase.CreateFolder(root, folders[i]);
            }

            root += "/" + folders[i];
        }
    }
    #endregion
}
#endif