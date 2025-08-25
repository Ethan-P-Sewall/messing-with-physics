using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector : MonoBehaviour
{
    controllableCharacter moveThis;
    bool clickable = false;
    LayerMask mask = 256;
    public void SetCharacter(controllableCharacter set)
    {
        moveThis = set;
    }

    void OnMouseUpAsButton()
    {
        if (clickable)
        {
            moveThis.transform.position = transform.position + Vector3.up;
            GameManager.instance.CleanThisUp("Selector");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        clickable = true;
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
