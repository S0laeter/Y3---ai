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
    public enum CooldownMode
    {
        OnEnter,
        OnSucceeded,
        OnFailed,
    }

    /// <summary>
    /// CD
    /// 进入或完成节点时进入冷却，冷却完成前条件装饰器返回false。  
    /// </summary>
    [DisplayName("Cooldown")]
    [SerializationAlias("Megumin.AI.BehaviorTree.Cooldown")]
    [HelpURL(URL.WikiDecorator + "Cooldown_Decorator")]
    public class Cooldown_Decorator : ConditionDecorator, IPreDecorator, IPostDecorator, IConditionDecorator
    {
        public RefVar_Float CooldownTime = new RefVar_Float() { value = 5 };
        public CooldownMode Mode = CooldownMode.OnEnter;

        protected double nextCanEnterTime = -1;

        protected override bool OnCheckCondition(object options = null)
        {
            return Time.time > nextCanEnterTime;
        }

        public Status AfterNodeExit(Status result, object options = null)
        {
            if (Mode == CooldownMode.OnSucceeded && result == Status.Succeeded)
            {
                nextCanEnterTime = Time.time + CooldownTime;
            }
            else if (Mode == CooldownMode.OnFailed && result == Status.Failed)
            {
                nextCanEnterTime = Time.time + CooldownTime;
            }

            return result;
        }

        public void BeforeNodeEnter(object options = null)
        {
            if (Mode == CooldownMode.OnEnter)
            {
                nextCanEnterTime = Time.time + CooldownTime;
            }
        }
    }
}
