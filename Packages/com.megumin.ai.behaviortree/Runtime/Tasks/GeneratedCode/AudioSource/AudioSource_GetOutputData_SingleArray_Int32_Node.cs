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
    [Icon("AudioSource Icon")]
    [DisplayName("AudioSource_GetOutputData")]
    [Category("UnityEngine/AudioSource")]
    [AddComponentMenu("GetOutputData(Single[], Int32)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class AudioSource_GetOutputData_SingleArray_Int32_Node : BTActionNode<UnityEngine.AudioSource>
    {
        [Space]
        public Megumin.Binding.RefVar_Float_Array samples;
        public Megumin.Binding.RefVar_Int channel;

        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.AudioSource)MyAgent).GetOutputData(samples, channel);
            return Status.Succeeded;
        }
    }
}




