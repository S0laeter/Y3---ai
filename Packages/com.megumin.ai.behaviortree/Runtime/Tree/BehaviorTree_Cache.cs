﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Megumin.Binding;

namespace Megumin.AI.BehaviorTree
{
    public partial class BehaviorTree
    {
        protected int version = 0;

        protected int nodeIndexVersion = -1;

        public void UpdateNodeIndexDepth(bool force = false)
        {
            if (force == false && nodeIndexVersion == version)
            {
                return;
            }

            foreach (var item in AllNodes)
            {
                item.Index = -1;
                item.Depth = -1;
            }

            var index = 0;
            void SetNodeIndex(BTNode node, int depth = 0)
            {
                if (node == null)
                {
                    return;
                }

                node.Index = index;
                node.Depth = depth;

                index++;

                if (node is BTParentNode parentNode)
                {
                    var nextDepth = depth + 1;
                    foreach (var child in parentNode.Children)
                    {
                        SetNodeIndex(child, nextDepth);
                    }
                }
            }

            SetNodeIndex(StartNode);
            nodeIndexVersion = version;
        }

        [Obsolete("Use InitAddObjTreeElement and InitAddObjNotTreeElement instead.")]
        public void InitAddTreeRefObj<T>(T value)
        {
            InitAddObjTreeElement(value);

            InitAddObjToInterfaceCollection(value);
        }

        /// <summary>
        /// 初始化添加直接属于树的节点装饰器，但是不包含节点成员值的node类型。
        /// 当节点引用其他节点时，可能会出现多次添加。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void InitAddObjTreeElement<T>(T value)
        {
            if (value is BTNode node)
            {
                AddNode(node);
            }

            if (value is BehaviorTreeElement element)
            {
                element.Tree = this;
            }

            InitAddObjToInterfaceCollection(value);
        }

        /// <summary>
        /// 添加间接引用对象。
        /// 添加不是树直接所属的树节点和装饰器到树中。
        /// 当节点引用其他节点时，可能会出现多次添加。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void InitAddObjNotTreeElement<T>(T value)
        {
            InitAddObjToInterfaceCollection(value);
        }


        /// <summary>
        /// 测试是否含有接口，并缓存到指定容器中，方便同意调用。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public void InitAddObjToInterfaceCollection<T>(T value)
        {
            if (value is IBindingParseable parseable)
            {
                AllBindingParseable.Add(parseable);
            }

            if (value is IBindAgentable bindAgentable)
            {
                AllBindAgentable.Add(bindAgentable);
            }

            if (value is IAwakeable awakeable)
            {
                AllAwakeable.Add(awakeable);
            }

            if (value is IStartable startable)
            {
                AllStartable.Add(startable);
            }

            if (value is IResetable resetable)
            {
                AllResetable.Add(resetable);
            }
        }

        public void InitAddVariable<T>(T value)
        {
            if (value is IRefable variable)
            {
                Variable.Table.Add(variable);
            }

            InitAddObjToInterfaceCollection(value);
        }

        /// <summary>
        /// 用于编辑中UndoRedo时实例对象改变。
        /// </summary>
        public void ReCacheDic()
        {
            GuidDic.Clear();
            foreach (var node in AllNodes)
            {
                GuidDic.Add(node.GUID, node);
                if (node.GUID == Asset?.StartNodeGUID)
                {
                    StartNode = node;
                }
            }
        }
    }
}



