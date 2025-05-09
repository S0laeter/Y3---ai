using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions
{
    public static Action<PlayerBehavior> UpdatePlayerHealthBar;
    public static Action<PlayerBehavior> UpdatePlayerStaminaBar;

    public static Action<LevelManager> UpdateRoundTimer;

    public static Action TimeOut;
    public static Action<PlayerBehavior> GameOver;

}
