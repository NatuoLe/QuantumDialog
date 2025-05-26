using System.IO;
using cfg;
using UnityEngine;
using SimpleJSON;
using UnityEditor;

namespace QuantumDialog.Editor
{
#if UNITY_EDITOR
    // Unity Editor 编译或启动时自动调用
    [InitializeOnLoad]
    public static class MainDataEditorLoader
    {
        private static string _dataHash;

        public class MainDataEditor
        {
            private const string ConfigDir = "Assets/_DynamicGroups/Configs/DataTables/";
            private static MainDataEditor _instance;
            private static Tables _database;
            private static System.DateTime _lastWriteTime;
            public static MainDataEditor Instance => _instance ??= new MainDataEditor();

            public Tables Database
            {
                get
                {
                    // 每次访问时检查数据是否需要重新加载
                    if (CheckIfDataChanged())
                    {
                        ReloadData();
                    }

                    return _database;
                }
            }
            private MainDataEditor()
            {
                ReloadData();
            }
            private bool CheckIfDataChanged()
            {
                var lastWriteTime = Directory.GetLastWriteTime(ConfigDir);
                return lastWriteTime != _lastWriteTime;
            }
            public void ReloadData()
            {
                _database = new Tables(Utils.ReadAllConfigsEditor);
                _lastWriteTime = Directory.GetLastWriteTime(ConfigDir);
                Debug.Log($"[Editor] 数据已重新加载:Last Write Time{_lastWriteTime}");
            }
        }
    }
#endif
}