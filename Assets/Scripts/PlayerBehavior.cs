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

    public bool isDead = false;

    //this is to decide which action to do next
    public CombatIntention combatIntention;

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
            combatIntention = CombatIntention.Attack;
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
            
            currentStamina += 1 * Time.deltaTime;
            Actions.UpdatePlayerStaminaBar(this);
        }

    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            currentHp = Mathf.Clamp(currentHp - damage, 0f, maxHp);
            Actions.UpdatePlayerHealthBar(this);
        }
    }

    public void ConsumeStamina(float stamina)
    {
        currentStamina = Mathf.Clamp(currentStamina - stamina, 0f, maxStamina);
        Actions.UpdatePlayerStaminaBar(this);
    }






    private void Lose()
    {
        stateMachine.SetNextState(new LoseState());

        Actions.GameOver(otherPlayer);
    }

    private void Win()
    {
        stateMachine.SetNextState(new WinState());

        Actions.GameOver(this);
    }










}
