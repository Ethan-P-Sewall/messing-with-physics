using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public enum ProjectileMode { Arc, Linear }
    Rigidbody RB; float horizDist;
    Vector3 initPos; Vector3 targetPos; Vector3 callThisX;
    [SerializeField] float speed, heightMod; int wait = 0;
    [SerializeField] ProjectileMode path;

    //TODO: account for elevation difference
    public void Initialize()
    {
        RB = GetComponent<Rigidbody>();
        initPos.x = transform.position.x;
        initPos.z = transform.position.z;
    }
    public void SetTarget(Vector3 target)
    {
        targetPos = target;
        horizDist = Vector3.Distance(new Vector3(initPos.x, 0, initPos.z), new Vector3(target.x, 0, target.z));
        callThisX = Vector3.Normalize(new Vector3(targetPos.x - initPos.x, 0, targetPos.z - initPos.z));
        if (path == ProjectileMode.Linear)
        {
            transform.LookAt(targetPos);
        }
    }

    void FixedUpdate()
    {
        if (path == ProjectileMode.Arc)
        {
            float unlerp = Mathf.InverseLerp(initPos.x, targetPos.x, transform.position.x); //how far it is along projected path
            Vector3 v = callThisX;
            v.y = heightMod * (1 - ((2 * (unlerp * horizDist)) / horizDist));
            v *= speed;
            RB.velocity = v;
        }
        else
        {
            wait++;
            if (wait > 3)
            {
                RB.AddRelativeForce(new Vector3(0, 0, speed), ForceMode.VelocityChange);
                Destroy(this);
            }
        }
    }
}
