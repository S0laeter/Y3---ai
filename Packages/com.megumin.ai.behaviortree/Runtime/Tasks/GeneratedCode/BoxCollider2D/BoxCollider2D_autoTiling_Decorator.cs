﻿///********************************************************************************************************************************
///The code on this page is generated by the code generator, do not manually modify.
///CodeGenerator: Megumin.CSCodeGenerator.  Version: 1.0.2
///CodeGenericBy: Megumin.AI.BehaviorTree.Editor.NodeGenerator
///CodeGenericSourceFilePath: Packages/com.megumin.ai.behaviortree/Editor/Generator/NodeGeneraotr.asset
///********************************************************************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Megumin.AI.BehaviorTree
{
    [Icon("BoxCollider2D Icon")]
    [DisplayName("BoxCollider2D_autoTiling")]
    [Category("UnityEngine/BoxCollider2D")]
    [AddComponentMenu("autoTiling")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class BoxCollider2D_autoTiling_Decorator : ConditionDecorator<UnityEngine.BoxCollider2D>
    {
        [Space]
        public Megumin.Binding.RefVar_Bool SaveValueTo;

        public override bool CheckCondition(object options = null)
        {
            var result = ((UnityEngine.BoxCollider2D)MyAgent).autoTiling;

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return result;
        }

    }
}




