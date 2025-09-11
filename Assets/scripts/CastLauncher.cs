using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLauncher : MonoBehaviour
{
    [SerializeField] GameObject launchThis;

    public void Launch(Vector3 pos)
    {
        GameObject foo = Instantiate(launchThis, transform.position + (Vector3.up * 0.5f), transform.rotation);
        foo.GetComponent<ProjectileMotion>().Initialize();
        foo.GetComponent<ProjectileMotion>().SetTarget(pos);
    }
}
