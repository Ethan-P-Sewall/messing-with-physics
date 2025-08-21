using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killbox : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("ground"))
        {
            other.gameObject.AddComponent<despawn>();
        }
    }
}
