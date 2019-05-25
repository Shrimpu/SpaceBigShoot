using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Core))]
public class SimpleMove : MonoBehaviour
{
    Core core;

    private void Awake()
    {
        core = GetComponent<Core>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(core.stats.MovementVector * WorldData.speed * Time.deltaTime);
    }
}
