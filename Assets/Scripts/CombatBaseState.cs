using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatBaseState : State
{
    //this is to decide which action to do next
    public bool shouldAttack;
    public bool shouldDefend;
    public bool shouldMove;

    //duration doesnt need to be the same as animation length, make it slightly shorter to transition early to next attack
    protected float stateDuration;

    protected PlayerBehavior playerBehavior;
    protected CharacterController controller;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        playerBehavior = stateMachine.GetComponent<PlayerBehavior>();
        controller = playerBehavior.GetComponent<CharacterController>();

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

        Debug.Log("idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();



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

        controller.Move(Vector3.forward * 3f * Time.deltaTime);



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

        controller.Move(Vector3.back * 3f * Time.deltaTime);



    }

}

public class BlockState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        Debug.Log("blocking");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();



    }

}

public class SlipState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        Debug.Log("dodge");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();



    }

}

public class SwitchSideState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        Debug.Log("switch side");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();



    }

}