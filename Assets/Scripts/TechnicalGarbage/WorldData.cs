using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldData
{
    public const float speed = 1f; // this is the base speed the world moves towards the player

    public const int bottomScreen = -4;
    public const int topScreen = 13;

    public static float timeUntilLetterCycle = 0.4f;
    static float letterCycleSpeed = 4f;
    public static float LetterCycleSpeed
    {
        get { return 1f / letterCycleSpeed; }
    }
}