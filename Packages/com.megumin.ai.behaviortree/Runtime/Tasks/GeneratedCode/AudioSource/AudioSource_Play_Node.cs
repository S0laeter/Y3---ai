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
    [DisplayName("AudioSource_Play")]
    [Category("UnityEngine/AudioSource")]
    [AddComponentMenu("Play")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class AudioSource_Play_Node : BTActionNode<UnityEngine.AudioSource>
    {
        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.AudioSource)MyAgent).Play();
            return Status.Succeeded;
        }
    }
}




