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
    [Icon("PlayableDirector Icon")]
    [DisplayName("PlayableDirector_RebindPlayableGraphOutputs")]
    [Category("UnityEngine/PlayableDirector")]
    [AddComponentMenu("RebindPlayableGraphOutputs")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class PlayableDirector_RebindPlayableGraphOutputs_Node : BTActionNode<UnityEngine.Playables.PlayableDirector>
    {
        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.Playables.PlayableDirector)MyAgent).RebindPlayableGraphOutputs();
            return Status.Succeeded;
        }
    }
}




