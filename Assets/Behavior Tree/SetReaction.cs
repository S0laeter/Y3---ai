using System;
using System.Collections.Generic;
using System.ComponentModel;
using Megumin.AI;
using Megumin.AI.BehaviorTree;
using UnityEngine;

[Category("Action")]
public sealed class SetReaction : BTActionNode
{
    public string reactionType;

    private PlayerBehavior player;

    public float duration;
    private float time;

    protected override void OnEnter(BTNode from, object options = null)
    {
        player = this.GameObject.GetComponent<PlayerBehavior>();
        time = 0;

        //a really scuffed way to do probability
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        if (reactionType == "dodge" && randomNumber <= player.chanceToDodge)
        {
            player.stateMachine.SetNextState(new DodgeState());
        }
        else if (reactionType == "block" && randomNumber <= player.chanceToBlock)
        {
            player.stateMachine.SetNextState(new BlockState());
        }
        else
        {
            player.stateMachine.SetNextStateToMain();
        }
    }

    protected override Status OnTick(BTNode from, object options = null)
    {
        //state timer, if it's over return the "Succeed" bool, else keep running
        time += Time.deltaTime;

        if (time > duration)
        {
            player.stateMachine.SetNextStateToMain();
            return Status.Succeeded;
        }

        return Status.Running;
    }
}