using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blast : MonoBehaviour
{
    [SerializeField] float Radius;
    [SerializeField] float force;

    void OnCollisionEnter(Collision collision)
    {
        Collider[] foo = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < foo.Length; i++)
        {
            if (foo[i].gameObject.GetComponent<cube>())
            {
                foo[i].gameObject.GetComponent<cube>().GetExploded(transform.position - (Vector3.up * Radius * 0.5f), force, Radius);
            }
        }
        Destroy(gameObject);
    }
}
