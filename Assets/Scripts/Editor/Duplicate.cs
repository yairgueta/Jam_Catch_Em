using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Duplicate : EditorWindow
    {
        private string suffix = "@copy";
        [MenuItem("Window/Custom Duplicate")]
        private static void ShowWindow()
        {
            var window = GetWindow<Duplicate>();
            window.titleContent = new GUIContent("Custom Duplicate");
            window.Show();
        }

        private void OnGUI()
        {
            // GUILayout.TextArea(AssetDatabase.GetAssetPath(Selection.objects));
            Object[] os = Selection.objects;
            GUILayout.Label(os.Length + ((os.Length == 1) ? " Is " : " Are ") + "Selected");
            EditorGUI.BeginDisabledGroup(os.Length == 0);
            suffix = EditorGUILayout.TextField("Suffix: ", suffix);
            if (GUILayout.Button("Duplicate"))
            {
                foreach (var o in os)
                {
                    string path = AssetDatabase.GetAssetPath(o), name = Path.GetFileNameWithoutExtension(path);
                    
                    if (!AssetDatabase.CopyAsset(path, path.Replace(name, name+suffix)))
                    {
                        Debug.LogWarning("Couldn't duplicate "+name);
                    }
                    // Debug.Log(path.Replace(name, name+suffix));
                }
            }
            EditorGUI.EndDisabledGroup();
            
        }
    }
}