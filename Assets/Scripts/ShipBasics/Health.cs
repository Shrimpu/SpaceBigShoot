using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Core))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class Health : MonoBehaviour, IHit
{
    protected int health;

    public bool CanTakeDamage { get; set; } = true;

    AudioSource source;
    Core core;

    private void Awake()
    {
        core = GetComponent<Core>();
        source = GetComponent<AudioSource>();
        health = core.stats.health;
    }

    public virtual bool TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            PlaySound(core.feedback.HitSound);
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
            return true;
        }
        return false;
    }

    public void Die()
    {
        core.died?.Invoke();
        Score.Add(core.stats.points);
        Instantiate(core.feedback.deathEffect);
        PlaySound(core.feedback.deathSound);
        Destroy(gameObject, core.feedback.deathSound.length);
    }

    protected void PlaySound(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
}
