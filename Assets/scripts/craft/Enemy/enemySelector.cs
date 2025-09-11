using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySelector : MonoBehaviour
{
    EnemyBehavior moveThis;
    bool action = false;
    LayerMask mask = 256;

    public void SetEnemy(EnemyBehavior set)
    {
        moveThis = set;
    }

    public void Selected()
    {
        if (action)
        {
            moveThis.Moving(transform.position + Vector3.up);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        action = true;
        float foo = transform.position.y - moveThis.transform.position.y;
        if (foo > moveThis.jumpDist)
        {
            Destroy(gameObject);
        }
        else
        {
            if (Physics.Linecast(transform.position + Vector3.up, moveThis.transform.position + Vector3.up, mask))
            {
                Destroy(gameObject);
            }
        }
    }
}
