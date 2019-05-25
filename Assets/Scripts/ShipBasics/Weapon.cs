using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Ship/Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject projectile;

    public int damage = 1;
    public float projectileSpeed = 2f;
    public float spreadInDegrees = 0f;
    public float rawFirerate;
    public float Firerate { get { return 1f / rawFirerate; } }

    public AudioClip shootSound;

    public virtual void Move(ref Rigidbody rb, Vector3 dir)
    {
        rb.velocity = dir * projectileSpeed;
    }
}
