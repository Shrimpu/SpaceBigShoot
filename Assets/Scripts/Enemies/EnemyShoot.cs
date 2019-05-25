using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : Shoot
{
    private void Start()
    {
        StartCoroutine(FirerateTimer(firepointGroups[Iteration].weapon.Firerate));
    }

    protected override IEnumerator FirerateTimer(float time)
    {
        readyToShoot = false;
        yield return new WaitForSeconds(time);
        readyToShoot = true;
        TryToShoot();
    }
}
