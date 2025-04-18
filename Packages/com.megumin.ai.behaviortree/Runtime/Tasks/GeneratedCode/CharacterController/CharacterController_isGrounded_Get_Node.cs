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
    [Icon("CharacterController Icon")]
    [DisplayName("Get_CharacterController_isGrounded")]
    [Category("UnityEngine/CharacterController")]
    [AddComponentMenu("Get_isGrounded")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class CharacterController_isGrounded_Get_Node : BTActionNode<UnityEngine.CharacterController>
    {
        [Space]
        public Megumin.Binding.RefVar_Bool SaveValueTo;

        protected override Status OnTick(BTNode from, object options = null)
        {
            var result = ((UnityEngine.CharacterController)MyAgent).isGrounded;

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return Status.Succeeded;
        }

    }
}




