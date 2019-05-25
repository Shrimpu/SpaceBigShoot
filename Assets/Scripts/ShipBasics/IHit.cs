using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHit
{
    bool CanTakeDamage { get; }

    bool TakeDamage(int damage);
}
