﻿using System;
using System.ComponentModel;
using Megumin.Binding;
using Megumin.Reflection;
using UnityEngine;
using UnityEngine.Serialization;

namespace Megumin.AI.BehaviorTree
{
    //[FormerlySerializedAs("CheckBool")]
    [Serializable]
    [DisplayName("CheckBool")]
    [SerializationAlias("Megumin.AI.BehaviorTree.CheckBool")]
    [HelpURL(URL.WikiDecorator + "CheckBool_Decorator")]
    public class CheckBool_Decorator : ConditionDecorator, IConditionDecorator
    {
        public RefVar_Bool Value;

        protected override bool OnCheckCondition(object options = null)
        {
            return Value;
        }
    }
}

