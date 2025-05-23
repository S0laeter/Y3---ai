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
    [Icon("Animator Icon")]
    [DisplayName("Animator_SetFloat")]
    [Category("UnityEngine/Animator")]
    [AddComponentMenu("SetFloat(Int32, Single, Single, Single)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Animator_SetFloat_Int32_Single_Single_Single_Node : BTActionNode<UnityEngine.Animator>
    {
        [Space]
        public Megumin.Binding.RefVar_Int id;
        public Megumin.Binding.RefVar_Float value;
        public Megumin.Binding.RefVar_Float dampTime;
        public Megumin.Binding.RefVar_Float deltaTime;

        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.Animator)MyAgent).SetFloat(id, value, dampTime, deltaTime);
            return Status.Succeeded;
        }
    }
}




