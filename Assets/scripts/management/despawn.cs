using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class despawn : MonoBehaviour
{
    [SerializeField] bool ShouldSleep;
    [SerializeField] float timer = 1;
    float counter = 0; Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!ShouldSleep || RB.IsSleeping())
        {
            counter += Time.deltaTime;
        }
        if (counter > timer)
        {
            Destroy(gameObject);
        }
    }
}
