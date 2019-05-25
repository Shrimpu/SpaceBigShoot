using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : Shoot
{
    protected override bool CanShoot { get { return readyToShoot; } }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            TryToShoot();
        }

    }

    protected override void SpawnProjectile(int i)
    {
        GameObject projectile = Instantiate(firepointGroups[Iteration].weapon.projectile,
                    firepointGroups[Iteration].firepoints[i].position,
                    firepointGroups[Iteration].firepoints[i].rotation);

        projectile.AddComponent<Projectile>().Setup(firepointGroups[Iteration].weapon);
    }
}
