using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Weapon weapon;

    string targetTag;

    Rigidbody rb;

    public void Setup(Weapon weapon, string targetTag = "Enemy")
    {
        this.weapon = weapon;
        this.targetTag = targetTag;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        GetComponent<Collider>().isTrigger = true;
    }

    void FixedUpdate()
    {
        weapon.Move(ref rb, transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            IHit hit = other.GetComponent<IHit>();
            if (hit != null)
            {
                hit.TakeDamage(weapon.damage);
                Destroy(gameObject);
            }
        }
    }
}
