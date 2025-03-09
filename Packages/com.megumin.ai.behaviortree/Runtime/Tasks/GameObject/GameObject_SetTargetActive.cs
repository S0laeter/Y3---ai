﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.Binding;
using Megumin.AI.BehaviorTree;
using UnityEngine;

namespace Megumin.AI.BehaviorTree
{
    /// <summary>
    /// 游戏对象开关节点
    /// </summary>
    [Icon("GameObject Icon")]
    [DisplayName("SetTargetActive")]
    [Category("UnityEngine/GameObject")]
    [AddComponentMenu("SetTargetActive(Boolean)")]
    [HelpURL(URL.WikiTask + "GameObject_SetTargetActive")]
    public sealed class GameObject_SetTargetActive : BTActionNode
    {
        [Space]
        public RefVar_GameObject Target;

        [Space]
        public Megumin.Binding.RefVar_Bool value;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var go = (UnityEngine.GameObject)Target;
            if (go)
            {
                go.SetActive(value);
                return Status.Succeeded;
            }
            else
            {
                return Status.Failed;
            }
        }
    }
}




