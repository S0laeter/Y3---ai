using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using Megumin.Binding;
using UnityEngine;

public sealed class CheckOtherPlayerState : ConditionDecorator
{
    public string stateType;

    private PlayerBehavior otherPlayer;

    protected override bool OnCheckCondition(object options = null)
    {
        otherPlayer = this.GameObject.GetComponent<PlayerBehavior>().otherPlayer;

        switch (stateType)
        {
            case "hit head":
                if (otherPlayer.stateMachine.currentState.GetType() == typeof(LeftStraightState)
                || otherPlayer.stateMachine.currentState.GetType() == typeof(RightStraightState)
                || otherPlayer.stateMachine.currentState.GetType() == typeof(LeftHookState)
                || otherPlayer.stateMachine.currentState.GetType() == typeof(RightHookState))
                    return true;
                break;

            case "hit body":
                if (otherPlayer.stateMachine.currentState.GetType() == typeof(LeftBodyState)
                || otherPlayer.stateMachine.currentState.GetType() == typeof(RightBodyState))
                    return true;
                break;

            case "move forward":
                if (otherPlayer.stateMachine.currentState.GetType() == typeof(MoveForwardState))
                    return true;
                break;

            case "move back":
                if (otherPlayer.stateMachine.currentState.GetType() == typeof(MoveBackwardState))
                    return true;
                break;

            default:
                break;
        }

        return false;
    }
}