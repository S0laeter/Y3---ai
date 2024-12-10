using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public Gradient hpGradient;

    //timer and rounds
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI timerText;

    //announce end game
    public GameObject gameOverText;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //subscribing to actions
        Actions.UpdatePlayerHealthBar += UpdateHealthBar;
        Actions.UpdatePlayerStaminaBar += UpdateStaminaBar;
        Actions.UpdateRoundTimer += UpdateRoundTimer;
        Actions.GameOver += GameOver;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.UpdatePlayerHealthBar -= UpdateHealthBar;
        Actions.UpdatePlayerStaminaBar -= UpdateStaminaBar;
        Actions.UpdateRoundTimer -= UpdateRoundTimer;
        Actions.GameOver -= GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        gameOverText.SetActive(false);
    }

    private void UpdateHealthBar(PlayerBehavior player)
    {
        switch (player.playerID)
        {
            case 1:
                player1HpSlider.maxValue = player.maxHp;
                player1HpSlider.value = player.currentHp;
                player1HpFill.color = hpGradient.Evaluate(player1HpSlider.normalizedValue);
                break;
            case 2:
                player2HpSlider.maxValue = player.maxHp;
                player2HpSlider.value = player.currentHp;
                player2HpFill.color = hpGradient.Evaluate(player2HpSlider.normalizedValue);
                break;
            default:
                break;
        }
    }

    private void UpdateStaminaBar(PlayerBehavior player)
    {
        switch (player.playerID)
        {
            case 1:
                player1StaminaSlider.maxValue = player.maxStamina;
                player1StaminaSlider.value = player.currentStamina;
                break;
            case 2:
                player2StaminaSlider.maxValue = player.maxStamina;
                player2StaminaSlider.value = player.currentStamina;
                break;
            default:
                break;
        }
    }

    private void UpdateRoundTimer(LevelManager levelManager)
    {
        roundText.text = "ROUND " + levelManager.currentRound.ToString();

        //change color when near time out
        if (levelManager.currentTime <= 30f)
        {
            timerText.color = Color.red;
        }

        //update timer text, in minutes and seconds
        float minutes = Mathf.Clamp(Mathf.Floor(levelManager.currentTime / 60), 0f, 60f);
        float seconds = Mathf.Clamp(Mathf.Floor(levelManager.currentTime % 60), 0f, 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void GameOver(PlayerBehavior winner)
    {
        gameOverText.SetActive(true);

        if (winner == null)
            gameOverText.GetComponent<TextMeshProUGUI>().text = "game over, draw";
        else
            gameOverText.GetComponent<TextMeshProUGUI>().text = "game over, " + winner.name + " won";
    }


}
