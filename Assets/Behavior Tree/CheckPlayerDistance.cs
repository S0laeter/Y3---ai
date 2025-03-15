using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using Megumin.Binding;
using UnityEngine;

public sealed class CheckPlayerDistance : ConditionDecorator
{
    protected override bool OnCheckCondition(object options = null)
    {
        if (GameObject.GetComponent<PlayerBehavior>().distanceToOtherPlayer <= 1f)
            return true;

        return false;
    }
}