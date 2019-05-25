using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour // this is the data-center for all ships
{
    public delegate void Notice();
    public Notice died;

    public Stats stats;
    public Feedback feedback;
}
