using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using UnityEngine;

public sealed class CheckPlayerDistance : ConditionDecorator
{
    public GameObject otherPlayer;

    protected override bool OnCheckCondition(object options = null)
    {
        float distanceToOtherPlayer = Vector3.Distance(otherPlayer.transform.position, this.GameObject.transform.position);

        if (distanceToOtherPlayer > 1f)
            return true;

        return false;
    }
}