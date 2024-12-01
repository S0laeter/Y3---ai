using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentRound;
    public int maxRound;

    public float currentTime;
    public float maxTime = 180f;

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

}
