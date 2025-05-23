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
    [Icon("NavMeshAgent Icon")]
    [DisplayName("NavMeshAgent_GetAreaCost")]
    [Category("UnityEngine/NavMeshAgent")]
    [AddComponentMenu("GetAreaCost(Int32)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class NavMeshAgent_GetAreaCost_Int32_Node : BTActionNode<UnityEngine.AI.NavMeshAgent>
    {
        [Space]
        public Megumin.Binding.RefVar_Int areaIndex;

        [Space]
        public Megumin.Binding.RefVar_Float SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = ((UnityEngine.AI.NavMeshAgent)MyAgent).GetAreaCost(areaIndex);

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }
    }
}




