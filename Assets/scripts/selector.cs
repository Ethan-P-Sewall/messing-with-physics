using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector : MonoBehaviour
{
    controllableCharacter moveThis;

    public void SetCharacter(controllableCharacter set)
    {
        moveThis = set;
    }

    void OnMouseUpAsButton()
    {
        moveThis.transform.position = transform.position + Vector3.up;
        GameManager.instance.CleanThisUp("Selector");
    }
}
