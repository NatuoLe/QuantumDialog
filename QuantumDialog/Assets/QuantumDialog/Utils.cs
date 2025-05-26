using SimpleJSON;
using UnityEditor;
using UnityEngine;

namespace QuantumDialog
{
    public class Utils
    {
        public static JSONNode ReadAllConfigsEditor(string file)
        {
            string path = "Assets/_DynamicGroups/Configs/DataTables/" + file + ".json";

            // 使用 Unity Editor 的 AssetDatabase 加载 TextAsset
            TextAsset jsonAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);

            if (jsonAsset == null)
            {
                Debug.LogError($"无法加载 JSON 文件: {path}");
                return null;
            }

            return JSON.Parse(jsonAsset.text);
        }
    }
}