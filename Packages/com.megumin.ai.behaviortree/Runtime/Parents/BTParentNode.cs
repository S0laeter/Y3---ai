﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Megumin.AI.BehaviorTree
{
    public abstract class BTParentNode : BTNode
    {
        /// <summary>
        /// 这里必须使用泛型序列化，否则Undo/Redo 时元素会丢失自己的真实类型。notconnect 多层级颜色bug
        /// </summary>
        [HideInInspector]
        [SerializeReference]
        [FormerlySerializedAs("children")]
        public List<BTNode> Children = new();

        public bool ContainsChild(BTNode node)
        {
            foreach (BTNode child in Children)
            {
                if (child.GUID == node.GUID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 测试一个节点是不是自己的子代
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool IsDescendant(BTNode node, List<BTParentNode> parentPath = null)
        {
            if (node == null)
            {
                return false;
            }

            foreach (BTNode child in Children)
            {
                if (child.GUID == node.GUID)
                {
                    parentPath?.Add(this);
                    return true;
                }

                if (child is BTParentNode parentNode)
                {
                    var result = parentNode.IsDescendant(node, parentPath);
                    if (result)
                    {
                        parentPath?.Add(this);
                        return true;
                    }
                }
            }

            return false;
        }
    }

    //父节点支持泛型组件
    public abstract class BTParentNode<T> : BTParentNode, IHasMyAgent, IMyAgentable<T>
    {
        public T MyAgent { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        //如果T为接口，组件不能实现接口，使用代理类型。
        //实际使用发现，编写代理类型代码，和编写一个独立节点工作量差不多。
        //性能也会损失，所以放弃这个实现。
        //public string AdpterType = null;

        /// <summary>
        /// 验证MyAgent有效性，防止Tick过程中空引用异常
        /// </summary>
        [Space]
        [Tooltip("Verify MyAgent validity to prevent NullReferenceException in Tick process")]
        [FormerlySerializedAs("SafeMyAgent")]
        public bool VerifyMyAgent = true;

        /// <summary>
        /// 有时候MyAgent初始化晚于行为树，可能导致MyAgent组件无法获得。
        /// 这个开关用于在节点执行时重新BindMyAgent。
        /// </summary>
        [FormerlySerializedAs("RebindMyAgentBeforeCanExecute")]
        [Tooltip("Rebind myAgent before CanExecute. More performance overhead")]
        public bool AutoRebind = false;

        public bool HasMyAgent()
        {
            if (MyAgent == null)
            {
                return false;
            }

            if (MyAgent is UnityEngine.Object obj && !obj)
            {
                return false;
            }
            return true;
        }

        public override void BindAgent(object agent)
        {
            base.BindAgent(agent);

            BindMyAgent(true);
        }

        protected virtual void BindMyAgent(bool force = false)
        {
            if (force || HasMyAgent() == false)
            {
                if (Agent is T tAgent)
                {
                    MyAgent = tAgent;
                }
                else
                {
                    if (GameObject)
                    {
                        MyAgent = GameObject.GetComponentInChildren<T>();
                    }
                }
            }
        }

        public override bool CanExecute(object options = null)
        {
            if (AutoRebind)
            {
                BindMyAgent();
            }

            if (VerifyMyAgent)
            {
                if (HasMyAgent() == false)
                {
                    return false;
                }
            }

            return base.CanExecute(options);
        }
    }

}



