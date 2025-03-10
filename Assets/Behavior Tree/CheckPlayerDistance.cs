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

/*using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using Megumin.Binding;
using UnityEngine;

[Category("Action")]
public sealed class CheckPlayerDistance : BTActionNode
{
    public RefVar_GameObject otherPlayer;

    protected override Status OnTick(BTNode from, object options = null)
    {
        float distanceToOtherPlayer = Vector3.Distance(otherPlayer.Value.transform.position, GameObject.transform.position);
        Debug.Log(distanceToOtherPlayer);

        if (GameObject.GetComponent<PlayerBehavior>().distanceToOtherPlayer <= 1f)
            return Status.Succeeded;
        else
            return Status.Failed;

    }
}*/