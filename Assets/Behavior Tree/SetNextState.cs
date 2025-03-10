using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using UnityEngine;

[Category("Action")]
public sealed class SetNextState : BTActionNode
{
    private bool Success = true;
    
    private PlayerBehavior player;

    public string stateName;

    public float duration;
    private float time;

    protected override void OnEnter(BTNode from, object options = null)
    {
        player = this.GameObject.GetComponent<PlayerBehavior>();
        time = 0;

        switch (stateName)
        {
            case "left straight":
                player.stateMachine.SetNextState(new LeftStraightState());
                break;
            case "left hook":
                player.stateMachine.SetNextState(new LeftHookState());
                break;
            case "left body":
                player.stateMachine.SetNextState(new LeftBodyState());
                break;
            case "right straight":
                player.stateMachine.SetNextState(new RightStraightState());
                break;
            case "right hook":
                player.stateMachine.SetNextState(new RightHookState());
                break;
            case "right body":
                player.stateMachine.SetNextState(new RightBodyState());
                break;
            case "dodge":
                player.stateMachine.SetNextState(new DodgeState());
                break;
            case "block":
                player.stateMachine.SetNextState(new BlockState());
                break;
            case "move forward":
                player.stateMachine.SetNextState(new MoveForwardState());
                break;
            case "move back":
                player.stateMachine.SetNextState(new MoveBackwardState());
                break;
            default:
                player.stateMachine.SetNextState(new IdleState());
                break;
        }
    }

    protected override Status OnTick(BTNode from, object options = null)
    {
        //state timer, if it's over return the "Succeed" bool, else keep running
        time += Time.deltaTime;
        if (time > duration)
        {
            Success = true;
            return Success ? Status.Succeeded : Status.Failed;
        }

        return Status.Running;
    }
}