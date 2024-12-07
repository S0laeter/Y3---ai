using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatBaseState : State
{
    //duration doesnt need to be the same as animation length, make it slightly shorter to transition early to next attack
    protected float stateDuration;

    protected PlayerBehavior playerBehavior;
    protected CharacterController controller;
    protected Animator anim;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        playerBehavior = stateMachine.GetComponent<PlayerBehavior>();
        controller = playerBehavior.GetComponent<CharacterController>();
        anim = playerBehavior.GetComponent<Animator>();

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

    }

    public override void OnExit()
    {
        base.OnExit();

    }

}

public class CombatIdleState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("idle");
        Debug.Log("idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (playerBehavior.combatIntention == CombatIntention.Defend)
        {

        }
        else if (playerBehavior.combatIntention == CombatIntention.Attack)
        {

        }
        else if (playerBehavior.combatIntention == CombatIntention.MoveBackward)
        {
            stateMachine.SetNextState(new MoveBackwardState());
        }
        else if (playerBehavior.combatIntention == CombatIntention.MoveForward)
        {
            stateMachine.SetNextState(new MoveForwardState());
        }

    }

}

public class MoveForwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("move forward");
        Debug.Log("moving forward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(Vector3.forward * 3f * Time.deltaTime);

        //if no need to move anymore
        if (playerBehavior.combatIntention != CombatIntention.MoveForward)
            stateMachine.SetNextStateToMain();

    }

}

public class MoveBackwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("move backward");
        Debug.Log("moving backward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(Vector3.back * 3f * Time.deltaTime);

        //if no need to move anymore
        if (playerBehavior.combatIntention != CombatIntention.MoveBackward)
            stateMachine.SetNextStateToMain();

    }

}

public class BlockState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("block");
        Debug.Log("blocking");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if no need to block anymore
        if (playerBehavior.combatIntention != CombatIntention.Defend)
            stateMachine.SetNextStateToMain();

    }

}

public class SlipState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0f;

        anim.SetTrigger("dodge");
        Debug.Log("dodge");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class SwitchSideState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0f;

        anim.SetTrigger("switch");
        Debug.Log("switch side");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}