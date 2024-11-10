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
    protected Rigidbody rb;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        playerBehavior = GetComponent<PlayerBehavior>();
        rb = playerBehavior.GetComponent<Rigidbody>();

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

        rb.velocity = Vector3.forward * 0f;

        Debug.Log("idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //after state duration
        if (fixedTime >= stateDuration)
        {

        }

    }

}

public class MoveForwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        Debug.Log("moving forward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        rb.velocity = Vector3.forward * 100f * Time.deltaTime;

        //after state duration
        if (fixedTime >= stateDuration)
        {

        }

    }

}

public class MoveBackwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        Debug.Log("moving backward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        rb.velocity = Vector3.back * 100f * Time.deltaTime;

        //after state duration
        if (fixedTime >= stateDuration)
        {

        }

    }

}