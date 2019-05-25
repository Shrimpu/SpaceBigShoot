using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Ship/Stats")]
public class Stats : ScriptableObject
{
    public float speed;
    public int health;
    [Space]
    public Vector3 movementDirection;

    public Vector3 MovementVector
    {
        get { return movementDirection * speed; }
    }
    [Space]
    [Tooltip("Points awarded on defeat")]
    public int points;
}