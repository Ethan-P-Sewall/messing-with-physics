using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftComponent : MonoBehaviour
{
    public enum ComponentPart { Orb, Hat, LArm, RArm, LLeg, RLeg }
    [SerializeField] int maxHP; public float currentHP { get; private set; }
    [SerializeField] ComponentPart whatPart;
    [SerializeField] SpriteRenderer sprite;

    void Start()
    {
        currentHP = maxHP;
    }

    //todo: orb ignores damage when hat is present
    public void TakeDamage(float damage)
    {
        if (currentHP > 0)
        {
            if (whatPart == ComponentPart.Orb)
            {
                if (Hat())
                {
                    GetComponent<hat_connector>().hat.TakeDamage(damage);
                }
                else
                {
                    currentHP -= damage;
                    DamageColor();
                }
            }
            else
            {
                currentHP -= damage;
                DamageColor();
            }

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
                    if (transform.parent.GetComponent<controllableCharacter>())
                    {
                        transform.parent.GetComponent<controllableCharacter>().OrbFail();
                    }
                    else if (transform.parent.GetComponent<EnemyBehavior>())
                    {
                        transform.parent.GetComponent<EnemyBehavior>().OrbFail();
                    }
                    gameObject.AddComponent<Rigidbody>();
                }
                else if (whatPart == ComponentPart.Hat)
                {
                    transform.parent.GetComponent<hat_connector>().Disconnect();
                }
                transform.parent = null;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                gameObject.AddComponent<ExplodableObject>();
                gameObject.GetComponent<ExplodableObject>().SET(10);
                gameObject.AddComponent<despawn>();
                gameObject.GetComponent<despawn>().SET(15);
                Destroy(this);
            }
        }
    }
    void DamageColor()
    {
        float foo = Mathf.InverseLerp(0, maxHP, currentHP);
        float bar;

        if (foo > 0.83f)//white to blue
        {
            bar = Mathf.InverseLerp(0.83f, 1, foo);
            sprite.color = new Color(bar, bar, 1);
        }
        else if (foo > 0.66f)//blue to green
        {
            bar = Mathf.InverseLerp(0.66f, 0.83f, foo);
            sprite.color = new Color(0, 1 - bar, bar);
        }
        else if (foo > 0.5f)//green to yellow
        {
            bar = Mathf.InverseLerp(0.5f, 0.66f, foo);
            sprite.color = new Color(1 - bar, 1, 0);
        }
        else if (foo > 0.33f)//yellow to orange
        {
            bar = Mathf.InverseLerp(0.33f, 0.5f, foo);
            sprite.color = new Color(1, 0.5f + (bar * 0.5f), 0);
        }
        else if (foo > 0.16f)//orange to red
        {
            bar = Mathf.InverseLerp(0.16f, 0.33f, foo);
            sprite.color = new Color(1, (bar * 0.5f), 0);
        }
        else if (foo > 0)//red to black
        {
            bar = Mathf.InverseLerp(0, 0.16f, foo);
            sprite.color = new Color(bar, 0, 0);
        }
        else
        {
            sprite.color = Color.black;
        }
    }

    public ComponentPart WhatPart()
    {
        return whatPart;
    }

    bool Hat()
    {
        bool foo = true;

        if(whatPart == ComponentPart.Orb)
        {
            if(GetComponent<hat_connector>().hat == null)
            {
                foo = false;
            }
        }

        return foo;
    }
}
