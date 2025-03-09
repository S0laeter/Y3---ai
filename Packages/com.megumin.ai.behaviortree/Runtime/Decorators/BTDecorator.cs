﻿using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Megumin.AI.BehaviorTree
{
    public abstract class BTDecorator : BehaviorTreeElement, IDecorator
    {
        /// <summary>
        /// 物主节点
        /// </summary>
        public BTNode Owner { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
    }

    public abstract class BTDecorator<T> : BTDecorator, IHasMyAgent, IMyAgentable<T>
    {
        public T MyAgent { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

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
    }

    /// <summary>
    /// 条件装饰器
    /// </summary>
    public abstract class ConditionDecorator : BTDecorator, IAbortable, IConditionDecorator
    {
        [Space]
        public bool Invert = false;

        [field: SerializeField]
        public AbortType AbortType { get; set; }

        public bool LastCheckResult { get; protected set; }

        //每帧只求解一次。TODO，还没有考虑好要不要加
        //public int LastCheckTickCount { get; protected set; } = -1;
        //public bool CalOnceOnTick = false;

        public virtual bool CheckCondition(object options = null)
        {
            //if (CalOnceOnTick)
            //{
            //    if (Tree.TotalTickCount == LastCheckTickCount)
            //    {
            //        return LastCheckResult;
            //    }
            //}
            //LastCheckTickCount = Tree.TotalTickCount;

            LastCheckResult = OnCheckCondition(options);

            if (Invert)
            {
                LastCheckResult = !LastCheckResult;
            }

            return LastCheckResult;
        }

        protected virtual bool OnCheckCondition(object options = null)
        {
            return false;
        }
    }

    public abstract class ConditionDecorator<T> : ConditionDecorator, IHasMyAgent, IMyAgentable<T>
    {
        public T MyAgent { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

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

        public override bool CheckCondition(object options = null)
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

            return base.CheckCondition(options);
        }
    }


    public abstract class CompareDecorator<V> : ConditionDecorator
        where V : IComparable<V>
    {
        [Space]
        public CompareToMode Mode = CompareToMode.Equals;

        public abstract V GetResult();

        public abstract V GetCompareTo();

        protected override bool OnCheckCondition(object options = null)
        {
            var compareResult = GetResult().CompareTo(GetCompareTo());
            switch (Mode)
            {
                case CompareToMode.Equals:
                    return compareResult == 0;
                case CompareToMode.Less:
                    return compareResult < 0;
                case CompareToMode.Greater:
                    return compareResult > 0;
                case CompareToMode.LessEquals:
                    return compareResult <= 0;
                case CompareToMode.GreaterEquals:
                    return compareResult >= 0;
                case CompareToMode.NotEqual:
                    return compareResult != 0;
                default:
                    break;
            }

            return base.OnCheckCondition(options);
        }
    }

    public abstract class CompareDecorator<T, V> : ConditionDecorator<T>
         where V : IComparable<V>
    {
        [Space]
        public CompareToMode Mode = CompareToMode.Equals;

        public abstract V GetResult();

        public abstract V GetCompareTo();

        protected override bool OnCheckCondition(object options = null)
        {
            var compareResult = GetResult().CompareTo(GetCompareTo());
            switch (Mode)
            {
                case CompareToMode.Equals:
                    return compareResult == 0;
                case CompareToMode.Less:
                    return compareResult < 0;
                case CompareToMode.Greater:
                    return compareResult > 0;
                case CompareToMode.LessEquals:
                    return compareResult <= 0;
                case CompareToMode.GreaterEquals:
                    return compareResult >= 0;
                case CompareToMode.NotEqual:
                    return compareResult != 0;
                default:
                    break;
            }

            return base.OnCheckCondition(options);
        }
    }

    public abstract class StringEqualsDecorator : ConditionDecorator
    {
        [Space]
        public StringComparison ComparisonType = StringComparison.Ordinal;

        public abstract string GetResult();

        public abstract string GetCompareTo();

        protected override bool OnCheckCondition(object options = null)
        {
            return string.Equals(GetResult(), GetCompareTo(), ComparisonType);
        }
    }

    public abstract class StringEqualsDecorator<T> : ConditionDecorator<T>
    {
        [Space]
        public StringComparison ComparisonType = StringComparison.Ordinal;

        public abstract string GetResult();

        public abstract string GetCompareTo();

        protected override bool OnCheckCondition(object options = null)
        {
            return string.Equals(GetResult(), GetCompareTo(), ComparisonType);
        }
    }
}



