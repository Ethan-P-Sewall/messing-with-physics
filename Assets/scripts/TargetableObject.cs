using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObject : MonoBehaviour
{
    public static Vector3 lastClickedOnPos;

    void OnMouseUpAsButton()
    {
        lastClickedOnPos = transform.position;
        controllableCharacter.TriggerALaunch(lastClickedOnPos);
    }
}
