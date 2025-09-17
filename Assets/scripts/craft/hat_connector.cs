using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hat_connector : MonoBehaviour
{
    public CraftComponent hat;
    public CraftComponent orb;

    public void Disconnect()
    {
        hat = null;
        orb = null;
    }
}
