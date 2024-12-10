using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentRound;
    public int maxRound;

    public float currentTime;
    public float maxTime = 180f;
    private bool timeOut = false;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.GameOver -= OnGameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
        currentTime = maxTime;
        StartCoroutine(TimerCountdown());

        Actions.UpdateRoundTimer(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime <= 0 && !timeOut)
        {
            timeOut = true;
            StopCoroutine(TimerCountdown());
            Actions.TimeOut();
        }

    }





    public IEnumerator TimerCountdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;

            Actions.UpdateRoundTimer(this);
        }
    }






    private void OnGameOver(PlayerBehavior winner)
    {
        StopCoroutine(TimerCountdown());

        Debug.Log(winner.name + " won. its over, time to go home");
        
        StartCoroutine(WaitAndBackToMenu());
    }
    private IEnumerator WaitAndBackToMenu()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
    }

}
