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
    [Icon("ParticleSystem Icon")]
    [DisplayName("ParticleSystem_SetPlaybackState")]
    [Category("UnityEngine/ParticleSystem")]
    [AddComponentMenu("SetPlaybackState(PlaybackState)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class ParticleSystem_SetPlaybackState_PlaybackState_Node : BTActionNode<UnityEngine.ParticleSystem>
    {
        [Space]
        public Megumin.Binding.RefVar<UnityEngine.ParticleSystem.PlaybackState> playbackState;

        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.ParticleSystem)MyAgent).SetPlaybackState(playbackState);
            return Status.Succeeded;
        }
    }
}




