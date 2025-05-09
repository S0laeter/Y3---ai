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
    [DisplayName("Quaternion_RotateTowards")]
    [Category("UnityEngine/Quaternion")]
    [AddComponentMenu("RotateTowards(Quaternion, Quaternion, Single)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Quaternion_RotateTowards_Quaternion_Quaternion_Single_Node : BTActionNode
    {
        [Space]
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> from;
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> to;
        public Megumin.Binding.RefVar_Float maxDegreesDelta;

        [Space]
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> SaveValueTo;

        protected override Status OnTick(BTNode from1, object options = null)
        {
            var result = UnityEngine.Quaternion.RotateTowards(from, to, maxDegreesDelta);

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }
    }
}




