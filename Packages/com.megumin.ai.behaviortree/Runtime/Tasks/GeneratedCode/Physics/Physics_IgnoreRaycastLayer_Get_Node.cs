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
    [DisplayName("Get_Physics_IgnoreRaycastLayer")]
    [Category("UnityEngine/Physics")]
    [AddComponentMenu("Get_IgnoreRaycastLayer")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Physics_IgnoreRaycastLayer_Get_Node : BTActionNode
    {
        [Space]
        public Megumin.Binding.RefVar_Int SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = UnityEngine.Physics.IgnoreRaycastLayer;

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }

    }
}




