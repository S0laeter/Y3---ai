using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    protected StateMachine playerStateMachine;

    protected float currentHp;
    protected float maxHp;
    protected float currentStamina;
    protected float maxStamina;

    protected bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerStateMachine = GetComponent<StateMachine>();

        currentHp = maxHp;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }

    public void Die()
    {


        Debug.Log("someone died. its over, everyone go home");
    }

    private IEnumerator StaminaRegen()
    {
        currentStamina++;

        yield return new WaitForSeconds(1f);
    }

}
