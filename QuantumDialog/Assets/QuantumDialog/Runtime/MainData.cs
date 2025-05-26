using System.IO;
using cfg;
using UnityEngine;
using SimpleJSON;
using UnityEditor;

namespace QuantumDialog
{
    public class MainData : MonoBehaviour
    {
        public static MainData Instance;

        private void Awake()
        {
            Instance = this;
        }
        //database_excel
        public Tables Database;
        protected void Start()
        {
            Database = new cfg.Tables(ReadAllConfigsEditor);
        }
        public static JSONNode ReadAllConfigsEditor(string file)
        {
            string path = "Assets/_DynamicGroups/Configs/DataTables/" + file + ".json";

            // 这里没有第三方的加载方式 SO 使用 Unity Editor 的 AssetDatabase 加载 TextAsset
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