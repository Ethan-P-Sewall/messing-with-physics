using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodableObject : MonoBehaviour
{
    [SerializeField] float forceMultiplier;
    Rigidbody RB;
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    public void GetExploded(Vector3 pos, float power, float dist)
    {
        RB.AddExplosionForce(power * forceMultiplier, pos, dist);
    }

    public void SET(int set)
    {
        forceMultiplier = set;
    }
}
