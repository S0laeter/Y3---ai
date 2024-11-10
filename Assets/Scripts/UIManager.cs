using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //p1 stuffs
    public Slider player1HpSlider;
    public Image player1HpFill;

    public Slider player1StaminaSlider;

    //p2 stuffs
    public Slider player2HpSlider;
    public Image player2HpFill;

    public Slider player2StaminaSlider;

    public Gradient gradient;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //subscribing to actions
        Actions.UpdatePlayerHealthBar += UpdateHealthBar;
        Actions.UpdatePlayerStaminaBar += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.UpdatePlayerHealthBar -= UpdateHealthBar;
        Actions.UpdatePlayerStaminaBar -= UpdateStaminaBar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthBar(PlayerBehavior player)
    {
        switch (player.playerID)
        {
            case 1:
                player1HpSlider.maxValue = player.GetMaxHp();
                player1HpSlider.value = player.GetCurrentHp();
                player1HpFill.color = gradient.Evaluate(player1HpSlider.normalizedValue);
                break;
            case 2:

                break;
            default:
                break;
        }
    }

    public void UpdateStaminaBar(PlayerBehavior player)
    {
        switch (player.playerID)
        {
            case 1:
                player1StaminaSlider.maxValue = player.GetMaxStamina();
                player1StaminaSlider.value = player.GetCurrentStamina();
                break;
            case 2:

                break;
            default:
                break;
        }
    }

}
