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
    [DisplayName("Animator_SetIKRotation")]
    [Category("UnityEngine/Animator")]
    [AddComponentMenu("SetIKRotation(AvatarIKGoal, Quaternion)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Animator_SetIKRotation_AvatarIKGoal_Quaternion_Node : BTActionNode<UnityEngine.Animator>
    {
        [Space]
        public Megumin.Binding.RefVar<UnityEngine.AvatarIKGoal> goal;
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> goalRotation;

        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.Animator)MyAgent).SetIKRotation(goal, goalRotation);
            return Status.Succeeded;
        }
    }
}




