﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Megumin.Binding;
using Megumin.Reflection;
using UnityEngine;

namespace Megumin.AI.BehaviorTree
{
    /// <summary>
    /// 保证同名锁同一时间只能有一个Task执行
    /// </summary>
    [DisplayName("Lock")]
    [SerializationAlias("Megumin.AI.BehaviorTree.Lock")]
    [HelpURL(URL.WikiDecorator + "Lock_Decorator")]
    public class Lock_Decorator : ConditionDecorator, IPostDecorator, IPreDecorator, IConditionDecorator
    {
        public RefVar<string> lockName;
        public Status AfterNodeExit(Status result, object options = null)
        {
            Tree.LockDic.Remove(lockName);
            return result;
        }

        public void BeforeNodeEnter(object options = null)
        {
            if (string.IsNullOrEmpty(lockName))
            {
                lockName = Owner.InstanceID;
            }

            Tree.LockDic.Add(lockName, this);
        }

        protected override bool OnCheckCondition(object options = null)
        {
            Tree.LockDic.TryGetValue(lockName, out var result);
            return result == this;
        }
    }
}
