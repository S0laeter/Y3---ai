using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using UnityEngine;

[Category("Action")]
public sealed class SetMovement : BTActionNode
{
    private bool Success = true;

    private PlayerBehavior player;

    public string direction;

    public float duration;
    private float time;

    protected override void OnEnter(BTNode from, object options = null)
    {
        player = this.GameObject.GetComponent<PlayerBehavior>();
        time = 0;

        if (player.stateMachine.currentState.GetType() == typeof(WinState)
            || player.stateMachine.currentState.GetType() == typeof(LoseState))
        {
            Success = false;
        }
        else
        {
            switch (direction)
            {
                case "forward":
                    player.stateMachine.SetNextState(new MoveForwardState());
                    break;
                case "backward":
                    player.stateMachine.SetNextState(new MoveBackwardState());
                    break;
                default:
                    player.stateMachine.SetNextStateToMain();
                    break;
            }
        }

    }

    protected override Status OnTick(BTNode from, object options = null)
    {
        //state timer, if it's over return the "Succeed" bool, else keep running
        time += Time.deltaTime;

        if (Success == false
            || player.stateMachine.currentState.GetType() == typeof(HitHeadState)
            || player.stateMachine.currentState.GetType() == typeof(HitBodyState))
        {
            return Status.Failed;
        }

        if (time > duration)
        {
            return Status.Succeeded;
        }

        return Status.Running;
    }
}