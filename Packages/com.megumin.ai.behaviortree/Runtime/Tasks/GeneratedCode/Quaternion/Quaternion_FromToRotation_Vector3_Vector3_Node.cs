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
    [DisplayName("Quaternion_FromToRotation")]
    [Category("UnityEngine/Quaternion")]
    [AddComponentMenu("FromToRotation(Vector3, Vector3)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Quaternion_FromToRotation_Vector3_Vector3_Node : BTActionNode
    {
        [Space]
        public Megumin.Binding.RefVar_Vector3 fromDirection;
        public Megumin.Binding.RefVar_Vector3 toDirection;

        [Space]
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = UnityEngine.Quaternion.FromToRotation(fromDirection, toDirection);

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }
    }
}




