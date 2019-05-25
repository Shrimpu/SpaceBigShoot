using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Feedback", menuName = "Ship/Feedback")]
public class Feedback : ScriptableObject
{
    public GameObject deathEffect;
    public AudioClip deathSound;
    public AudioClip HitSound;
}
