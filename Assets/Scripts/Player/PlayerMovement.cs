using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ScreenLoop))]
[RequireComponent(typeof(Core))]
public class PlayerMovement : MonoBehaviour
{
    public float forwardSpeedIncrease = 1.2f;
    public float tilt = 20;
    [Space]
    public float flyInTime = 0.2f;

    bool flyingIntoScreen = false;

    public bool CanMove
    {
        get
        {
            return !flyingIntoScreen;
        }
    }

    Rigidbody rb;
    Core core;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        core = GetComponent<Core>();
        GetComponent<ScreenLoop>().positionFlipped += ScreenflipReciever;
    }
    private void Start()
    {
        // failsafe
        rb.useGravity = false;
        core.died += () => { this.enabled = false; };
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (CanMove)
        {
            Vector3 dir = GetInputVector();
            dir.z = dir.z * forwardSpeedIncrease;

            rb.velocity = dir * core.stats.speed;
            rb.rotation = Quaternion.Euler(0, 0, dir.x * -tilt);
            // calmps the players top and bottom position
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, WorldData.bottomScreen, WorldData.topScreen));
        }
    }

    Vector3 GetInputVector()
    {
        Vector3 dir = new Vector3(Input.GetAxis("H"), 0, Input.GetAxis("V"));

        //  Artifact!!!
        if (dir.magnitude == 0)
            dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        // Artifact!!!

        return dir;
    }

    void ScreenflipReciever(float dir)
    {
        if (isActiveAndEnabled)
            StartCoroutine(FlyIntoScreen(dir));
    }
    // this is so the player cannot wrap around the screen and simply leave
    IEnumerator FlyIntoScreen(float dir)
    {
        flyingIntoScreen = true;
        rb.velocity = Vector3.right * -dir * core.stats.speed;
        yield return new WaitForSeconds(flyInTime);
        flyingIntoScreen = false;
    }
}
