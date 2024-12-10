using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatBaseState : State
{
    //duration doesnt need to be the same as animation length, make it slightly shorter to transition early to next attack
    protected float stateDuration;

    protected PlayerBehavior player;
    protected CharacterController controller;
    protected Animator anim;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //getting stuffs
        player = stateMachine.GetComponent<PlayerBehavior>();
        controller = player.GetComponent<CharacterController>();
        anim = player.GetComponent<Animator>();

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
        Debug.Log(player.name + " idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //should try to defend
        if (player.combatIntention == CombatIntention.Defend)
        {

        }
        //should try to attack
        else if (player.combatIntention == CombatIntention.Attack)
        {
            switch (UnityEngine.Random.Range(0, 6))
            {
                case 0:
                    stateMachine.SetNextState(new LeftStraightState());
                    break;
                case 1:
                    stateMachine.SetNextState(new LeftHookState());
                    break;
                case 2:
                    stateMachine.SetNextState(new LeftBodyState());
                    break;
                case 3:
                    stateMachine.SetNextState(new RightStraightState());
                    break;
                case 4:
                    stateMachine.SetNextState(new RightHookState());
                    break;
                case 5:
                    stateMachine.SetNextState(new RightBodyState());
                    break;
                default:
                    break;
            }
        }
        //should move backward
        else if (player.combatIntention == CombatIntention.MoveBackward)
        {
            stateMachine.SetNextState(new MoveBackwardState());
        }
        //should move backward
        else if (player.combatIntention == CombatIntention.MoveForward)
        {
            stateMachine.SetNextState(new MoveForwardState());
        }

    }

}

public class LoseState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("lose");
        Debug.Log(player.name + " lost");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveForward)
            stateMachine.SetNextStateToMain();

    }

}

public class WinState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        anim.SetTrigger("win");
        Debug.Log(player.name + " won");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveForward)
            stateMachine.SetNextStateToMain();

    }

}

public class MoveForwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 5f;

        anim.SetTrigger("move forward");
        Debug.Log(player.name + " moving forward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(Vector3.forward * 3f * Time.deltaTime);

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveForward)
            stateMachine.SetNextStateToMain();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class MoveBackwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 5f;

        anim.SetTrigger("move backward");
        Debug.Log(player.name + " moving backward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(Vector3.back * 3f * Time.deltaTime);

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveBackward)
            stateMachine.SetNextStateToMain();
        
        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class BlockState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 3f;

        anim.SetTrigger("block");
        Debug.Log(player.name + " blocking");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if no need to block anymore
        if (player.combatIntention != CombatIntention.Defend)
            stateMachine.SetNextStateToMain();

        //this is here so that it wouldnt block forever
        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class DodgeState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.4f;

        anim.SetTrigger("dodge");
        Debug.Log(player.name + " dodge");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class SwitchState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.6f;

        anim.SetTrigger("switch");
        Debug.Log(player.name + " switch");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class HitBodyState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.18f;

        anim.SetTrigger("hit body");
        Debug.Log(player.name + " received body hit");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class HitHeadState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.18f;

        anim.SetTrigger("hit head");
        Debug.Log(player.name + " received head hit");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class LeftStraightState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.35f;

        anim.SetTrigger("left straight");
        Debug.Log(player.name + " uses left straight");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class LeftHookState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.55f;

        anim.SetTrigger("left hook");
        Debug.Log(player.name + " uses left hook");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class LeftBodyState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.55f;

        anim.SetTrigger("left body");
        Debug.Log(player.name + " uses left body");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class RightStraightState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.45f;

        anim.SetTrigger("right straight");
        Debug.Log(player.name + " uses right straight");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class RightHookState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.65f;

        anim.SetTrigger("right hook");
        Debug.Log(player.name + " uses right hook");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}

public class RightBodyState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.65f;

        anim.SetTrigger("right body");
        Debug.Log(player.name + " uses right body");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= stateDuration)
            stateMachine.SetNextStateToMain();

    }

}