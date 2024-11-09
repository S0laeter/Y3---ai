using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombatBaseState : State
{

    //duration doesnt need to be the same as animation length, make it slightly shorter to transition early to next attack
    protected bool shouldCombo;
    protected bool shouldSkill;
    protected int attackIndex;
    protected int skillIndex;
    protected bool shouldUlt;
    protected float attackDuration;

    protected bool shouldDash;
    protected float dashDuration;
    protected float dashCooldown;

    protected PlayerBehavior playerBehavior;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        playerBehavior = GetComponent<PlayerBehavior>();

        //subscribing to actions

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void OnExit()
    {
        base.OnExit();

        //unsubscribing to actions

    }


}