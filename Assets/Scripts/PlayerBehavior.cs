using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatIntention
{
    Idle,
    MoveForward,
    MoveBackward,
    Attack,
    Defend
}

public class PlayerBehavior : MonoBehaviour
{
    public int playerID;

    public PlayerBehavior otherPlayer;

    public StateMachine stateMachine;

    public float currentHp;
    public float maxHp = 100f;
    public float currentStamina;
    public float maxStamina = 100f;

    private bool isDead = false;

    //this is to decide which action to do next
    public CombatIntention combatIntention;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.TimeOut += OnTimerOut;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.TimeOut -= OnTimerOut;
    }

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        currentHp = maxHp;
        Actions.UpdatePlayerHealthBar(this);
        currentStamina = maxStamina;
        Actions.UpdatePlayerStaminaBar(this);
    }

    // Update is called once per frame
    void Update()
    {

        //quick manual testing, delete later
        if (Input.GetMouseButton(0))
        {
            combatIntention = CombatIntention.MoveForward;
        }
        else if (Input.GetMouseButton(1))
        {
            combatIntention = CombatIntention.MoveBackward;
        }
        else
        {
            combatIntention = CombatIntention.Defend;
        }



        if (currentHp <= 0 && !isDead)
        {
            currentHp = 0;
            isDead = true;
            Lose();
            return;
        }

        if (currentStamina < maxStamina)
        {
            //stamina doesnt regen while blocking
            if (stateMachine.currentState.GetType() == typeof(BlockState))
                return;
            
            currentStamina += 3f * Time.deltaTime;
            Actions.UpdatePlayerStaminaBar(this);
        }

    }





    public void TakeHit(float hitDamage, string hitType)
    {
        if (!isDead)
        {
            //if blocking, and has enough stamina to tank the hit
            if (stateMachine.currentState.GetType() == typeof(BlockState) && currentStamina >= hitDamage)
            {
                ConsumeStamina(hitDamage);
                return;
            }
            //if switching
            else if (stateMachine.currentState.GetType() == typeof(SwitchState))
            {
                return;
            }
            //if dodging
            else if (stateMachine.currentState.GetType() == typeof(DodgeState))
            {
                if (hitType == "low")
                {
                    TakeDamage(hitDamage * 1.2f);
                }
            }
            //if doing anything else
            else
            {
                if (hitType == "high")
                {
                    TakeDamage(hitDamage * 1.2f);
                    stateMachine.SetNextState(new HitHeadState());
                }
                else if (hitType == "low")
                {
                    TakeDamage(hitDamage);
                    stateMachine.SetNextState(new HitBodyState());
                }
            }

        }
    }




    public void TakeDamage(float damage)
    {
        currentHp = Mathf.Clamp(currentHp - damage * 1f, 0f, maxHp);
        Actions.UpdatePlayerHealthBar(this);
    }
    public void ConsumeStamina(float stamina)
    {
        currentStamina = Mathf.Clamp(currentStamina - stamina, 0f, maxStamina);
        Actions.UpdatePlayerStaminaBar(this);
    }






    public void SpawnHitboxHigh(float damage)
    {
        //instantiate or use object pool here
    }
    public void SpawnHitboxLow(float damage)
    {
        //instantiate or use object pool here
    }






    private void Lose()
    {
        stateMachine.SetNextState(new LoseState());

        Actions.GameOver(otherPlayer);
    }
    private void OnTimerOut()
    {
        if (this.currentHp > otherPlayer.currentHp)
        {
            stateMachine.SetNextState(new WinState());
            Actions.GameOver(this);
        }
        else if (this.currentHp == otherPlayer.currentHp)
        {
            Actions.GameOver(null);
        }
        
    }










}
