using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int playerID;

    protected StateMachine playerStateMachine;

    protected float currentHp;
    protected float maxHp = 100f;
    protected float currentStamina;
    protected float maxStamina = 100f;

    protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStateMachine = GetComponent<StateMachine>();

        currentHp = maxHp;
        Actions.UpdatePlayerHealthBar(this);
        currentStamina = maxStamina;
        Actions.UpdatePlayerStaminaBar(this);
    }

    // Update is called once per frame
    void Update()
    {

        //quick manual testing
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ConsumeStamina(10);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            TakeDamage(10);
        }
        if (Input.GetMouseButton(0))
        {
            playerStateMachine.SetNextState(new MoveForwardState());
        }
        else if (Input.GetMouseButton(1))
        {
            playerStateMachine.SetNextState(new MoveBackwardState());
        }
        else
        {
            playerStateMachine.SetNextStateToMain();
        }



        if (currentHp <= 0 && !isDead)
        {
            currentHp = 0;
            isDead = true;
            Die();
            return;
        }

        if (currentStamina < maxStamina)
        {
            StartCoroutine(StaminaRegen());
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

    private void Die()
    {


        Debug.Log(this.name + " died. its over, everyone go home");
    }

    private IEnumerator StaminaRegen()
    {
        currentStamina++;
        Actions.UpdatePlayerStaminaBar(this);

        yield return new WaitForSeconds(1f);
    }



    public float GetCurrentHp() {  return currentHp; }
    public float GetMaxHp() {  return maxHp; }
    public float GetCurrentStamina() {  return currentStamina; }
    public float GetMaxStamina() { return maxStamina; }


}
