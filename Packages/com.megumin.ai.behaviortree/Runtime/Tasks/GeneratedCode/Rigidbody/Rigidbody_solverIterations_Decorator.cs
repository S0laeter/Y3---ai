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
    [Icon("Rigidbody Icon")]
    [DisplayName("Rigidbody_solverIterations")]
    [Category("UnityEngine/Rigidbody")]
    [AddComponentMenu("solverIterations")]
    [CodeGeneratorInfo(Name = "Megumin.CSCodeGenerator")]
    public sealed class Rigidbody_solverIterations_Decorator : CompareDecorator<UnityEngine.Rigidbody, int>
    {
        [Space]
        public Megumin.Binding.RefVar_Int CompareTo;

        [Space]
        public Megumin.Binding.RefVar_Int SaveValueTo;

        public override int GetResult()
        {
            var result = ((UnityEngine.Rigidbody)MyAgent).solverIterations;

            if (SaveValueTo != null)
            {
                SaveValueTo.Value = result;
            }

            return result;
        }

        public override int GetCompareTo()
        {
            return CompareTo;
        }

    }
}




