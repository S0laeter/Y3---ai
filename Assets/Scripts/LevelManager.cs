using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentRound;
    public int maxRound;

    public float currentTime;
    public float maxTime = 180f;

    private void OnEnable()
    {
        //subscribing to actions
        Actions.GameOver += GameOver;
    }

    private void OnDisable()
    {
        //unsubscribing to actions
        Actions.GameOver -= GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
        currentTime = maxTime;

        Actions.UpdateRoundTimer(this);

    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void GameOver(PlayerBehavior winner)
    {


        Debug.Log(winner.name + " won. its over, time to go home");
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Menu");
    }

}
