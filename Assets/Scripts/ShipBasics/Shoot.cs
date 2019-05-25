using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Core))]
[RequireComponent(typeof(AudioSource))]
public abstract class Shoot : MonoBehaviour
{
    [Tooltip("The tag of the object it shoots towards")]
    public string targetTag = "Player";
    [Space]
    [Tooltip("each group fires separetely in a cycle. two groups will alternate and one group will burst")]
    public FirepointGroup[] firepointGroups;

    [System.Serializable]
    public class FirepointGroup
    {
        public Weapon weapon;
        public Transform[] firepoints;
    }

    protected Core core;
    protected AudioSource source;

    int iteration = 0;
    public int Iteration { get { return iteration % firepointGroups.Length; } set { iteration = value; } }
    protected bool readyToShoot = true;
    protected virtual bool CanShoot { get { return readyToShoot; } }

    protected virtual void Awake()
    {
        core = GetComponent<Core>();
        source = GetComponent<AudioSource>();
    }

    protected virtual void TryToShoot()
    {
        if (CanShoot)
        {
            // readies next round
            StartCoroutine(FirerateTimer(firepointGroups[Iteration].weapon.Firerate));
            // plays sound
            if (core.feedback != null)
            {
                source.clip = firepointGroups[Iteration].weapon.shootSound;
                source.Play();
            }
            // fires all bullets from the firepointgroup
            for (int i = 0; i < firepointGroups[Iteration].firepoints.Length; i++)
            {
                SpawnProjectile(i);
            }
            // changes firepointGroup
            Iteration++;
        }
    }

    protected virtual void SpawnProjectile(int i)
    {
        Vector3 rotation = firepointGroups[Iteration].firepoints[i].rotation.ToEulerAngles();
        Quaternion projectileRotation = Quaternion.Euler(rotation.x, rotation.y + 
            Random.Range(firepointGroups[Iteration].weapon.spreadInDegrees, -firepointGroups[Iteration].weapon.spreadInDegrees) / 2f,
            rotation.z);

        GameObject projectile = Instantiate(firepointGroups[Iteration].weapon.projectile,
                    firepointGroups[Iteration].firepoints[i].position,
                    projectileRotation);

        projectile.AddComponent<Projectile>().Setup(firepointGroups[Iteration].weapon, targetTag);

    }

    protected virtual IEnumerator FirerateTimer(float time)
    {
        readyToShoot = false;
        yield return new WaitForSeconds(time);
        readyToShoot = true;
    }
}
