using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatState
{
    Idle,
    Won,
    Lost
}

public class PlayerBehavior : MonoBehaviour
{
    public int playerID;

    public PlayerBehavior otherPlayer;
    public float distanceToOtherPlayer;

    public float chanceToDodge;
    public float chanceToBlock;

    public StateMachine stateMachine;
    public CharacterController controller;
    public Animator anim;

    public float currentHp;
    public float maxHp = 100f;
    public float currentStamina;
    public float maxStamina = 100f;

    //this is to decide which action to do next
    public CombatState combatState;

    public bool isEndgame = false;

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
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        chanceToDodge = 0;
        chanceToBlock = 0;

        currentHp = maxHp;
        Actions.UpdatePlayerHealthBar(this);
        currentStamina = maxStamina;
        Actions.UpdatePlayerStaminaBar(this);
    }

    // Update is called once per frame
    void Update()
    {
        //if endgame, nevermind
        if (isEndgame)
            return;

        //calculate distance
        distanceToOtherPlayer = Vector3.Distance(otherPlayer.transform.position, transform.position);

        //if die
        if (currentHp <= 0 && combatState != CombatState.Lost)
        {
            currentHp = 0;

            Lose();
            otherPlayer.Win();
            Actions.GameOver(otherPlayer);

            return;
        }




        /*//ai behavior stuffs here, couldve made it another script but whatever
        switch (playerID)
        {



            //player 1, passive, reactive asf
            case 1:

                //combatIntention = CombatIntention.Idle;
                if (otherPlayer.combatIntention == CombatIntention.Attack)
                {
                    combatIntention = CombatIntention.Defend;
                }
                else
                {
                    if (currentStamina <= 20f)
                    {
                        combatIntention = CombatIntention.MoveBackward;
                    }
                    else
                    {
                        //move closer and beat em up
                        if (distanceToOtherPlayer > 1f)
                            combatIntention = CombatIntention.MoveForward;
                        else
                        {
                            combatIntention = CombatIntention.Attack;
                        }
                    }
                }

                break;





            //player 2, aggressive, kinda braindead
            case 2:

                if (currentStamina <= 20f)
                {
                    StartCoroutine(DoSomethingForSomeTime(CombatIntention.MoveBackward, 7f));
                }
                else if (currentStamina > 20f && combatIntention != CombatIntention.MoveBackward)
                {
                    //move closer and beat em up
                    if (distanceToOtherPlayer > 1f)
                        combatIntention = CombatIntention.MoveForward;
                    else
                    {
                        combatIntention = CombatIntention.Attack;
                    }
                }

                break;




            default:
                break;

        }*/




        //chance decrease over time
        if (chanceToDodge > 0)
            chanceToDodge -= Mathf.Clamp(1f * Time.deltaTime, 0f, 100f);
        if (chanceToBlock > 0)
            chanceToBlock -= Mathf.Clamp(1f * Time.deltaTime, 0f, 100f);

        //stamina behavior
        if (currentStamina < maxStamina)
        {
            currentStamina += 10f * Time.deltaTime;
            Actions.UpdatePlayerStaminaBar(this);
        }

    }



    public IEnumerator DoSomethingForSomeTime(CombatState whatNow, float forHowLong)
    {
        combatState = whatNow;

        yield return new WaitForSeconds(forHowLong);

        if (!isEndgame)
            combatState = CombatState.Idle;
    }

    //just in case player somehow drifts away from the centre line
    public void MoveToCentre()
    {
        Vector3 offset = new Vector3(0f, transform.position.y, transform.position.z) - transform.position;
        if (offset.magnitude > 0.1f)
        {
            controller.Move(offset * 10f * Time.deltaTime);
        }
    }





    public void TakeHit(float hitDamage, int hitType)
    {
        //if still alive, can take the hit
        if (combatState != CombatState.Lost)
        {
            //if blocking, and has enough stamina to tank the hit
            if (stateMachine.currentState.GetType() == typeof(BlockState) && currentStamina >= hitDamage)
            {
                controller.Move(-transform.forward * 0.1f);
                ConsumeStamina(hitDamage * 0.5f);
                return;
            }
            //if dodging
            else if (stateMachine.currentState.GetType() == typeof(DodgeState))
            {
                if (hitType == 0)
                {
                    TakeDamage(hitDamage * 1.2f);

                    IncreaseChanceToBlock();
                }
            }
            //if doing anything else
            else
            {
                if (hitType == 1)
                {
                    TakeDamage(hitDamage * 1.2f);
                    stateMachine.SetNextState(new HitHeadState());

                    IncreaseChanceToDodge();
                }
                else if (hitType == 0)
                {
                    TakeDamage(hitDamage);
                    stateMachine.SetNextState(new HitBodyState());

                    IncreaseChanceToBlock();
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

    //the more hit he takes, the higher the chance to dodge successfully
    public void IncreaseChanceToDodge()
    {
        chanceToDodge += Mathf.Clamp(30, 0f, 100f);

        //just to make sure, it fucks up sometimes
        if (chanceToDodge > 100)
            chanceToDodge = 100;
    }
    //the more hit he takes, the higher the chance to block successfully
    public void IncreaseChanceToBlock()
    {
        chanceToBlock += Mathf.Clamp(40, 0f, 100f);

        //just to make sure, it fucks up sometimes
        if (chanceToBlock > 100)
            chanceToBlock = 100;
    }











    private void Lose()
    {
        isEndgame = true;
        combatState = CombatState.Lost;
        stateMachine.SetNextState(new LoseState());
    }
    public void Win()
    {
        isEndgame = true;
        combatState = CombatState.Won;
        stateMachine.SetNextState(new WinState());
    }
    private void OnTimerOut()
    {
        if (this.currentHp > otherPlayer.currentHp)
        {
            Win();
            Actions.GameOver(this);
        }
        else if (this.currentHp == otherPlayer.currentHp)
        {
            Win();
            Actions.GameOver(null);
        }
        else if (this.currentHp < otherPlayer.currentHp)
        {
            Lose();
        }
        
    }










}
