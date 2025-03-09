﻿using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using Megumin;
using System.Collections.Generic;
using Megumin.Binding;
using System;
using UnityEditor.Experimental.GraphView;

namespace Megumin.AI.BehaviorTree.Editor
{
    public abstract class TreeElementWrapper : ScriptableObject,
        IRefVariableFinder,
        IRefSetterCollection,
        ITreeElementWrapper
    {
        [ReadOnlyInInspector]
        public MonoScript NodeScript;

        public BehaviorTree Tree => TreeView?.Tree;
        public abstract BehaviorTreeView TreeView { get; }

        public void GetAllElementsDerivedFrom(Type baseType, List<ITreeElement> refables)
        {
            if (Tree != null)
            {
                foreach (var node in Tree.AllNodes)
                {
                    if (baseType.IsAssignableFrom(node.GetType()))
                    {
                        refables.Add(node);
                    }

                    foreach (var d in node.Decorators)
                    {
                        if (baseType.IsAssignableFrom(d.GetType()))
                        {
                            refables.Add(d);
                        }
                    }
                }
            }
        }

        IEnumerable<IRefable> IRefVariableFinder.GetVariableTable()
        {
            return Tree?.Variable.Table;
        }

        public bool TryGetParam(string name, out IRefable variable)
        {
            variable = null;
            if (Tree?.Variable?.TryGetParam(name, out variable) ?? false)
            {
                return true;
            }
            return false;
        }

        public void Export(IRefable currentValue)
        {
            TreeView.Blackboard.AddNewVariable(currentValue);
        }

        public IEnumerable<(string OptionDisplay, object Value)> GetRefObjs(string filter = null, string[] category = null, Type[] type = null)
        {
            List<(string OptionDisplay, object Value)> result = new();
            var nodeFlag = false;
            var decoFlag = false;

            if (string.IsNullOrEmpty(filter) && category == null)
            {
                //没有设置类别时，使用节点
                nodeFlag = true;
            }

            if (filter?.Contains("node", StringComparison.OrdinalIgnoreCase) == true)
            {
                nodeFlag = true;
            }

            if (filter?.Contains("deco", StringComparison.OrdinalIgnoreCase) == true)
            {
                decoFlag = true;
            }

            foreach (var item in TreeView.graphElements)
            {
                if (nodeFlag && item is BehaviorTreeNodeView nodeView)
                {
                    result.Add(($"{nodeView.Node.Index,-2}  {nodeView.Node.ShortGUID}  {nodeView.title}", nodeView.Node));
                }

                if (decoFlag && item is BehaviorTreeDecoratorView decoratorView)
                {
                    result.Add(($"Deco {decoratorView.title} {decoratorView.Decorator.GUID}", decoratorView.Decorator));
                }
            }

            return result;
        }

        public bool EqualsRef(object lhs, object rhs)
        {
            if (lhs is BehaviorTreeElement le)
            {
                return le.GUID == (rhs as BehaviorTreeElement)?.GUID;
            }

            if (rhs is BehaviorTreeElement re)
            {
                return re.GUID == (lhs as BehaviorTreeElement)?.GUID;
            }

            return lhs == rhs;
        }
    }

    public class NodeWrapper : TreeElementWrapper
    {
        [SerializeReference]
        public BTNode Node;

        public override BehaviorTreeView TreeView => View?.TreeView;

        public BehaviorTreeNodeView View { get; internal set; }

        [Editor]
        public void Test()
        {
            if (View.outputContainer.ClassListContains("unDisplay"))
            {
                View.outputContainer.RemoveFromClassList("unDisplay");
            }
            else
            {
                View.outputContainer.AddToClassList("unDisplay");
            }
        }

        public VariableTable GetVariableTable()
        {
            return View?.TreeView?.Tree?.Variable;
        }


        //TODO
        //private void OnValidate()
        //{
        //    this.LogMethodName();
        //    //Undo 也会触发这个函数
        //    View.TreeView.UndoRecord($"Inspector Changed");
        //}
    }

    [CustomEditor(typeof(NodeWrapper), true, isFallback = false)]
    public class NodeWrapperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //this.DrawButtonBeforeDefaultInspector();
            var wrapper = (NodeWrapper)target;

            if (wrapper?.View != null)
            {
                //if (GUILayout.Button($"Open Script"))
                //{
                //    wrapper.View.OpenScript();
                //}

                var hasLogger = wrapper.View.Node.TraceListener != null;

                GUILayout.BeginHorizontal();

                using (new EditorGUI.DisabledScope(hasLogger || !Application.isPlaying))
                {
                    if (GUILayout.Button($"Enable Logger"))
                    {
                        if (wrapper.View.Node != null)
                        {
                            wrapper.View.Node.TraceListener = new UnityTraceListener();
                        }
                    }
                }

                using (new EditorGUI.DisabledScope(!hasLogger))
                {
                    if (GUILayout.Button($"Disable Logger"))
                    {
                        if (wrapper.View.Node != null)
                        {
                            wrapper.View.Node.TraceListener = null;
                        }
                    }
                }

                GUILayout.EndHorizontal();

                GUILayout.Space(10);
            }

            //内部使用了EditorGUI.BeginChangeCheck();
            //用这种方法检测是否面板更改，触发UndoRecord
            if (DrawDefaultInspector())
            {
                //这里值已经改变了，再Record已经来不及了
                //Todo BUG, Undo时没办法回退ChangeVersion，造成编辑器未保存状态无法消除
                //TODO, 打开关闭foldout也会触发，需要过滤掉。
                wrapper.View.TreeView.IncrementChangeVersion($"Inspector Changed");
                wrapper.View.TreeView.RefreshAllNodeEnabled();
                wrapper?.View?.ReloadView();
            }

            //this.DrawButtonAfterDefaultInspector();

            //if (GUILayout.Button("TestUSS"))
            //{
            //    var name = UssClassConst.dataWarning;
            //    wrapper.View.SetToClassList(name, !wrapper.View.ClassListContains(name));
            //}
        }
    }
}
