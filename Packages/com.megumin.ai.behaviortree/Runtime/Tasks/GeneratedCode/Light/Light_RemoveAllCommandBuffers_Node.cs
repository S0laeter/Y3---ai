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
    [Icon("Light Icon")]
    [DisplayName("Light_RemoveAllCommandBuffers")]
    [Category("UnityEngine/Light")]
    [AddComponentMenu("RemoveAllCommandBuffers")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Light_RemoveAllCommandBuffers_Node : BTActionNode<UnityEngine.Light>
    {
        protected override Status OnTick(BTNode from, object options = null)
        {
            ((UnityEngine.Light)MyAgent).RemoveAllCommandBuffers();
            return Status.Succeeded;
        }
    }
}




