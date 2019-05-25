using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLoop : MonoBehaviour // should be called ScreenWrap. couldn't remember the word.
{
    public delegate void PositionFlipped(float newSide);
    public PositionFlipped positionFlipped;

    bool sendDelegate = true;

    private void OnBecameInvisible()
    {
        transform.position = ReverseX(transform.position);
        if (sendDelegate)
            positionFlipped?.Invoke(Mathf.Sign(transform.position.x));
    }

    Vector3 ReverseX(Vector3 vector)
    {
        return new Vector3(-vector.x, vector.y, vector.z);
    }

    private void OnApplicationQuit()
    {
        // prevents shutdown error
        sendDelegate = false;
    }
}
