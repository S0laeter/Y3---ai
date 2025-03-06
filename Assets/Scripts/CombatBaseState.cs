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
        controller = player.controller;
        anim = player.anim;

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

        Debug.Log(player.name + " idle");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //stick to the centre line
        player.MoveToCentre();

        //should try to dodge
        if (player.combatIntention == CombatIntention.Dodge)
        {
            stateMachine.SetNextState(new DodgeState());
        }
        //should try to defend
        else if (player.combatIntention == CombatIntention.Block)
        {
            stateMachine.SetNextState(new BlockState());
        }
        //should try to attack high
        else if (player.combatIntention == CombatIntention.AttackHigh)
        {
            switch (UnityEngine.Random.Range(0, 4))
            {
                case 0:
                    stateMachine.SetNextState(new LeftStraightState());
                    break;
                case 1:
                    stateMachine.SetNextState(new LeftHookState());
                    break;
                case 2:
                    stateMachine.SetNextState(new RightStraightState());
                    break;
                case 3:
                    stateMachine.SetNextState(new RightHookState());
                    break;
                default:
                    break;
            }
        }
        //should try to attack low
        else if (player.combatIntention == CombatIntention.AttackLow)
        {
            switch (UnityEngine.Random.Range(0, 2))
            {
                case 0:
                    stateMachine.SetNextState(new LeftBodyState());
                    break;
                case 1:
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

        //stick to the centre line
        player.MoveToCentre();

        anim.SetTrigger("move forward");
        Debug.Log(player.name + " moving forward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(player.transform.forward * 1.5f * Time.deltaTime);

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveForward)
        {
            stateMachine.SetNextStateToMain();
        }

        if (fixedTime >= stateDuration)
        {
            stateMachine.SetNextStateToMain();
        }

    }

}

public class MoveBackwardState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 5f;

        //stick to the centre line
        player.MoveToCentre();

        anim.SetTrigger("move backward");
        Debug.Log(player.name + " moving backward");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        controller.Move(-player.transform.forward * 1f * Time.deltaTime);

        //if no need to move anymore
        if (player.combatIntention != CombatIntention.MoveBackward)
        {
            stateMachine.SetNextStateToMain();
        }

        if (fixedTime >= stateDuration)
        {
            stateMachine.SetNextStateToMain();
        }
            

    }

}

public class BlockState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 2f;

        anim.SetTrigger("block");
        Debug.Log(player.name + " blocking");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        //if no need to block anymore
        if (player.combatIntention != CombatIntention.Block)
        {
            stateMachine.SetNextStateToMain();
        }

        //this is here so that it wouldnt be blocking forever
        if (fixedTime >= stateDuration)
        {
            stateMachine.SetNextStateToMain();
        }

    }

}

public class DodgeState : CombatBaseState
{
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        stateDuration = 0.4f;

        player.ConsumeStamina(8f);

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

        stateDuration = 0.66f;

        player.ConsumeStamina(15f);

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

        player.ConsumeStamina(8f);

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

        player.ConsumeStamina(10f);

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

        player.ConsumeStamina(10f);

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

        player.ConsumeStamina(8f);

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

        player.ConsumeStamina(10f);

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

        player.ConsumeStamina(10f);

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