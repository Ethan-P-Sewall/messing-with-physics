using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftComponent : MonoBehaviour
{
    public enum ComponentPart { Orb, Hat, LArm, RArm, LLeg, RLeg }
    [SerializeField] int maxHP; public int currentHP { get; private set; }
    [SerializeField] ComponentPart whatPart;

    void Start()
    {
        currentHP = maxHP;
    }

    //todo: orb ignores damage when hat is present
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            if (gameObject.GetComponent<SpringJoint>())
            {
                Destroy(gameObject.GetComponent<SpringJoint>());
            }
            if (gameObject.GetComponent<CastLauncher>())
            {
                Destroy(gameObject.GetComponent<CastLauncher>());
            }
            if (whatPart == ComponentPart.Orb)
            {
                transform.parent.GetComponent<controllableCharacter>().OrbFail();
            }
            transform.parent = null;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            gameObject.AddComponent<ExplodableObject>();
            gameObject.GetComponent<ExplodableObject>().SET(40);
            Destroy(this);
        }
    }

    public ComponentPart WhatPart()
    {
        return whatPart;
    }

    [ContextMenu("EXPLODE")]void EXPLODE()
    {
        TakeDamage(9999);
    }
}
