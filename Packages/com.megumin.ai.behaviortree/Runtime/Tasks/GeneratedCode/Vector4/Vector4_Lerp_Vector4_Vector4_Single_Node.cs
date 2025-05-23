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
    [DisplayName("Vector4_Lerp")]
    [Category("UnityEngine/Vector4")]
    [AddComponentMenu("Lerp(Vector4, Vector4, Single)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Vector4_Lerp_Vector4_Vector4_Single_Node : BTActionNode
    {
        [Space]
        public Megumin.Binding.RefVar_Vector4 a;
        public Megumin.Binding.RefVar_Vector4 b;
        public Megumin.Binding.RefVar_Float t;

        [Space]
        public Megumin.Binding.RefVar_Vector4 SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = UnityEngine.Vector4.Lerp(a, b, t);

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }
    }
}




