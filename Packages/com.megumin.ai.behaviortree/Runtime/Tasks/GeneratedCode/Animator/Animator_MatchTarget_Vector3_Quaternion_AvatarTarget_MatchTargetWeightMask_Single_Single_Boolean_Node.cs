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
    [DisplayName("Animator_MatchTarget")]
    [Category("UnityEngine/Animator")]
    [AddComponentMenu("MatchTarget(Vector3, Quaternion, AvatarTarget, MatchTargetWeightMask, Single, Single, Boolean)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Animator_MatchTarget_Vector3_Quaternion_AvatarTarget_MatchTargetWeightMask_Single_Single_Boolean_Node : BTActionNode<UnityEngine.Animator>
    {
        [Space]
        public Megumin.Binding.RefVar_Vector3 matchPosition;
        public Megumin.Binding.RefVar<UnityEngine.Quaternion> matchRotation;
        public Megumin.Binding.RefVar<UnityEngine.AvatarTarget> targetBodyPart;
        public Megumin.Binding.RefVar<UnityEngine.MatchTargetWeightMask> weightMask;
        public Megumin.Binding.RefVar_Float startNormalizedTime;
        public Megumin.Binding.RefVar_Float targetNormalizedTime;
        public Megumin.Binding.RefVar_Bool completeMatch;

        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.Animator)MyAgent).MatchTarget(matchPosition, matchRotation, targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
            return Status.Succeeded;
        }
    }
}




