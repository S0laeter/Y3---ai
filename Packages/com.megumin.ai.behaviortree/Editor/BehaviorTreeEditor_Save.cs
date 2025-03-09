﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Megumin.AI.BehaviorTree.Editor
{
    public partial class BehaviorTreeEditor
    {

        public int SaveVersion = 0;

        double lastSaveClick;
        public void SaveAsset()
        {
            double delta = EditorApplication.timeSinceStartup - lastSaveClick;
            if (delta > 0.5 || delta < 0)
            {
                if (delta > 0)
                {
                    lastSaveClick = EditorApplication.timeSinceStartup;
                }
                SaveAsset(false);
            }
            else
            {
                //短时间内多次点击，强制保存
                lastSaveClick = EditorApplication.timeSinceStartup + 3;
                SaveAsset(true);
            }
        }

        public void SaveAsset(bool force = false)
        {
            if (TreeView?.SOTree?.ChangeVersion == SaveVersion && !force)
            {
                if (BehaviorTreeEditor.EditorLog)
                {
                    Debug.Log($"Asset file no unsavedChanges.");
                    //Debug.Log($"没有需要保存的改动。");
                }

                return;
            }

            if (CurrentAsset == null)
            {
                CurrentAsset = CreateScriptObjectTreeAssset<BehaviorTreeAsset_1_1>();
            }

            if (CurrentAsset == null)
            {
                Debug.LogError($"Save Asset Error! Asset file not found.");
                //Debug.Log($"保存资源失败，没有找到Asset文件");
                return;
            }

            var success = CurrentAsset.SaveTree(TreeView.Tree);
            if (success)
            {
                EditorUtility.SetDirty(CurrentAsset.AssetObject);
                AssetDatabase.SaveAssetIfDirty(CurrentAsset.AssetObject);
                AssetDatabase.Refresh();

                Debug.Log($"Save Asset Success.");
                //Debug.Log($"保存资源成功");
                SaveVersion = TreeView.SOTree.ChangeVersion;
                UpdateHasUnsavedChanges();
            }
            else
            {
                Debug.LogError($"Save Asset Error!");
                //Debug.Log($"保存资源失败");
            }

            CheckGUID();
        }

        public static void CheckGUID()
        {
            var all = CollectAllAsset<BehaviorTreeAsset_1_1>();
            if (all != null)
            {
                var g = from elem in all
                        group elem by elem.obj.GUID;
                var gs = all.GroupBy(elem => elem.obj.GUID).Where(g => g.Count() > 1);
                foreach (var item in gs)
                {
                    var str = $"Guid is same. {item.Key}";
                    foreach (var item1 in item)
                    {
                        str += $"  Path:{item1.path}";
                    }
                    Debug.LogError(str);
                }
            }
        }

        public T CreateScriptObjectTreeAssset<T>()
            where T : ScriptableObject, IBehaviorTreeAsset
        {
            var path = EditorUtility.SaveFilePanelInProject("保存", "BTtree", "asset", "test");
            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log(path);
                var treeAsset = ScriptableObject.CreateInstance<T>();
                treeAsset.SaveTree(TreeView.Tree);
                AssetDatabase.CreateAsset(treeAsset, path);
                AssetDatabase.Refresh();

                SelectTree(treeAsset);
                return treeAsset;
            }

            return null;
        }

        public bool TryCreateTreeAssset<T>(out T asset)
            where T : ScriptableObject, IBehaviorTreeAsset
        {
            var path = EditorUtility.SaveFilePanelInProject("保存", "BTtree", "asset", "test");
            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log(path);

                //要先删除原有资源才行，不然会导致资产不刷新，要重启编辑器才能刷新资源
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();

                var treeAsset = ScriptableObject.CreateInstance<T>();
                AssetDatabase.CreateAsset(treeAsset, path);
                AssetDatabase.Refresh();
                AssetDatabase.ImportAsset(path);
                asset = treeAsset;

                return true;
            }

            asset = default;
            return false;
        }

        private void SaveTreeAsJson(DropdownMenuAction obj)
        {
            var path = EditorUtility.SaveFilePanelInProject("保存", "BTJson", "json", "test");
            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log(path);
                TextAsset json = new TextAsset("{Tree}");
                AssetDatabase.CreateAsset(json, path);
                AssetDatabase.Refresh();
            }
        }

        /// <summary>
        /// 一键打开项目中所有行为树资产
        /// </summary>
        public void OpenAllAssetsInProject()
        {
            var all = CollectAllAsset<BehaviorTreeAsset_1_1>();
            foreach (var asset in all)
            {
                OnOpenAsset(asset.obj);
            }
        }

        public void CloseAllEditorInProject()
        {
            BehaviorTreeEditor[] array = Resources.FindObjectsOfTypeAll(typeof(BehaviorTreeEditor)) as BehaviorTreeEditor[];
            foreach (var item in array)
            {
                item.Close();
            }
        }

        public void CloseOtherEditor()
        {
            BehaviorTreeEditor[] array = Resources.FindObjectsOfTypeAll(typeof(BehaviorTreeEditor)) as BehaviorTreeEditor[];
            foreach (var item in array)
            {
                if (item != this)
                {
                    item.Close();
                }
            }
        }

        /// <summary>
        /// 一键重新序列化所有资产，为了处理改类名后资产没有及时更新问题。
        /// </summary>
        public void ForceReSaveAllAssetsInProject()
        {
            HotTypeAlias(true);
            var all = CollectAllAsset<BehaviorTreeAsset_1_1>();
            InitOption ForceReSaveInitOption = new()
            {
                AsyncInit = false,
                SharedMeta = false,
                LazyInitSubtree = true,
                UseGenerateCode = false,
            };

            foreach (var asset in all)
            {
                var tree = asset.obj.Instantiate(ForceReSaveInitOption);
                asset.obj.SaveTree(tree);
                EditorUtility.SetDirty(asset.obj);
                AssetDatabase.SaveAssetIfDirty(asset.obj);
                AssetDatabase.Refresh();
            }
        }

        static List<(T obj, string guid, long localId, string path)>
            CollectAllAsset<T>(List<string> collectFolder = null)
            where T : UnityEngine.Object
        {
            List<(T obj, string guid, long localId, string path)> list = new List<(T obj, string guid, long localId, string path)>();

#if UNITY_EDITOR

            var FindAssetsFolders = new string[] { "Assets", "Packages" };
            if (collectFolder != null)
            {
                FindAssetsFolders = collectFolder.ToArray();
            }

            string[] GUIDs = AssetDatabase.FindAssets($"t:{typeof(T).Name}",
                FindAssetsFolders);

            for (int i = 0; i < GUIDs.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(GUIDs[i]);
                var sos = AssetDatabase.LoadAllAssetsAtPath(path);
                foreach (var item in sos)
                {
                    if (item != null && item is T so)
                    {
                        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(so, out var tempGUID, out long localID);
                        list.Add((so, tempGUID, localID, path));
                    }
                }
            }
#endif

            return list;
        }


    }
}



