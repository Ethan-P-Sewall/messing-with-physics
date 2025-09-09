using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    Rigidbody RB;
    Vector3 initPos;
    Vector3 targetPos;
    Vector3 callThisX;
    float horizDist;
    [SerializeField] float speed, heightMod;

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
    }

    void FixedUpdate()
    {
        float unlerp = Mathf.InverseLerp(initPos.x, targetPos.x, transform.position.x); //how far it is along projected path
        Vector3 v = callThisX;
        v.y = heightMod * (1 - ((2 * (unlerp * horizDist)) / horizDist));
        v *= speed;
        RB.velocity = v;
    }
}
