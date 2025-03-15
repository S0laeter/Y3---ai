using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using Megumin.Binding;
using UnityEngine;

public sealed class CheckOtherPlayerState : ConditionDecorator
{
    public string stateName;

    private PlayerBehavior otherPlayer;

    protected override bool OnCheckCondition(object options = null)
    {
        otherPlayer = this.GameObject.GetComponent<PlayerBehavior>().otherPlayer;

        if (otherPlayer.stateMachine.currentState.GetType().ToString() == stateName)
            return true;

        return false;
    }
}