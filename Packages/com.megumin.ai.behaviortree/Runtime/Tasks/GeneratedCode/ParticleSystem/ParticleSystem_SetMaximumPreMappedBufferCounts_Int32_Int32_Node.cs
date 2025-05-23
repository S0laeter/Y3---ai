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
    [DisplayName("ParticleSystem_SetMaximumPreMappedBufferCounts")]
    [Category("UnityEngine/ParticleSystem")]
    [AddComponentMenu("SetMaximumPreMappedBufferCounts(Int32, Int32)")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class ParticleSystem_SetMaximumPreMappedBufferCounts_Int32_Int32_Node : BTActionNode<UnityEngine.ParticleSystem>
    {
        [Space]
        public Megumin.Binding.RefVar_Int vertexBuffersCount;
        public Megumin.Binding.RefVar_Int indexBuffersCount;

        protected override Status OnTick(BTNode from, object options = null)
        {
            UnityEngine.ParticleSystem.SetMaximumPreMappedBufferCounts(vertexBuffersCount, indexBuffersCount);
            return Status.Succeeded;
        }
    }
}




