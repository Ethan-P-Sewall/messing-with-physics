using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLauncher : MonoBehaviour
{
    [SerializeField] GameObject launchThis;

    public void Launch()
    {
        GameObject foo = Instantiate(launchThis, transform.position + (Vector3.up * 0.5f), transform.rotation);
        Vector3 v = TargetableObject.lastClickedOn.position;
        if (v.y < transform.position.y)
        {
            v.y += 0.5f;
        }
        foo.transform.LookAt(v);
    }
}
