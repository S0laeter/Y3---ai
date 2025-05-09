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
    [DisplayName("Get_AudioSource_mute")]
    [Category("UnityEngine/AudioSource")]
    [AddComponentMenu("Get_mute")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class AudioSource_mute_Get_Node : BTActionNode<UnityEngine.AudioSource>
    {
        [Space]
        public Megumin.Binding.RefVar_Bool SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = ((UnityEngine.AudioSource)MyAgent).mute;

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }

    }
}




