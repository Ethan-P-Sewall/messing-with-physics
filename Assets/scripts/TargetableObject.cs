using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetableObject : MonoBehaviour
{
    public static Transform lastClickedOn;

    void OnMouseUpAsButton()
    {
        lastClickedOn = transform;
        controllableCharacter.LaunchTarget();
    }
}
