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
    [DisplayName("SceneManager_GetActiveScene")]
    [Category("UnityEngine/SceneManager")]
    [AddComponentMenu("GetActiveScene")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class SceneManager_GetActiveScene_Node : BTActionNode
    {
        [Space]
        public Megumin.Binding.RefVar<UnityEngine.SceneManagement.Scene> SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }
    }
}




