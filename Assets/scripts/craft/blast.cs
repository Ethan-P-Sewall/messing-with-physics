using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blast : MonoBehaviour
{
    [SerializeField] float Radius;
    [SerializeField] float force;
    [SerializeField] float damage;
    [SerializeField] float forceFalloff; 
    // # = what % remains at the edge of the radius
    [SerializeField] float damageFalloff;
    [SerializeField] float[] spread;
    CraftComponent[] affectThese;
    List<CraftComponent> validTargets;
    List<CraftComponent> addedThese;
    bool activated = false;

    void Start()
    {
        affectThese = new CraftComponent[4];
        validTargets = new List<CraftComponent>();
        addedThese = new List<CraftComponent>();
    }
    void OnCollisionEnter(Collision collision)
    {
        Activate();
    }

    void Activate()
    {
        if (!activated)
        {
            activated = true;
            Collider[] foo = Physics.OverlapSphere(transform.position, Radius);
            for (int i = 0; i < foo.Length; i++)
            {
                if (foo[i].gameObject.GetComponent<cube>())
                {
                    foo[i].gameObject.GetComponent<cube>().GetExploded(transform.position - (Vector3.up * Radius * 0.5f), force, Radius, damage * spread[0] * GetFalloff(foo[i].transform.position, false));
                }
                else if (foo[i].gameObject.GetComponent<ExplodableObject>())
                {
                    foo[i].gameObject.GetComponent<ExplodableObject>().GetExploded(transform.position - (Vector3.up * Radius * 0.5f), force, Radius);
                }

                if (foo[i].gameObject.GetComponent<CraftComponent>())
                {
                    if (gameObject.CompareTag("PlayerCast") && foo[i].gameObject.CompareTag("EnemyComponent"))
                    {
                        validTargets.Add(foo[i].gameObject.GetComponent<CraftComponent>());
                    }
                    else if (gameObject.CompareTag("EnemyCast") && foo[i].gameObject.CompareTag("PlayerComponent"))
                    {
                        validTargets.Add(foo[i].gameObject.GetComponent<CraftComponent>());
                    }
                }
            }
            int bar = validTargets.Count;
            if (bar > 0)
            {
                for (int i = 0; i < Mathf.Min(4, validTargets.Count); i++)
                {
                    AddNthClosestPart(i);
                }
                switch (bar)
                {
                    case 1:
                        {
                            affectThese[0].TakeDamage(damage * spread[0] * GetFalloff(affectThese[0].transform.position, true));
                        }
                        break;
                    case 2:
                        {
                            affectThese[0].TakeDamage(damage * spread[0] * GetFalloff(affectThese[0].transform.position, true));
                            affectThese[1].TakeDamage(damage * spread[1] * GetFalloff(affectThese[1].transform.position, true));
                        }
                        break;
                    case 3:
                        {
                            affectThese[0].TakeDamage(damage * spread[0] * GetFalloff(affectThese[0].transform.position, true));
                            affectThese[1].TakeDamage(damage * spread[1] * GetFalloff(affectThese[1].transform.position, true));
                            affectThese[2].TakeDamage(damage * spread[2] * GetFalloff(affectThese[2].transform.position, true));
                        }
                        break;
                    default:
                        {
                            affectThese[0].TakeDamage(damage * spread[0] * GetFalloff(affectThese[0].transform.position, true));
                            affectThese[1].TakeDamage(damage * spread[1] * GetFalloff(affectThese[1].transform.position, true));
                            affectThese[2].TakeDamage(damage * spread[2] * GetFalloff(affectThese[2].transform.position, true));
                            affectThese[3].TakeDamage(damage * spread[3] * GetFalloff(affectThese[3].transform.position, true));
                        }
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    void AddNthClosestPart(int N)
    {
        float foo = 9999999;
        int index = 0;
        for (int i = 0; i < validTargets.Count; i++)
        {
            if (Vector3.Distance(transform.position, validTargets[i].transform.position) < foo)
            {
                if (!(addedThese.Contains(validTargets[i])))
                {
                    foo = Vector3.Distance(transform.position, validTargets[i].transform.position);
                    index = i;
                }
            }
        }
        affectThese[N] = validTargets[index];
        addedThese.Add(validTargets[index]);
    }

    float GetFalloff(Vector3 targetPos, bool damage)
    {
        float foo = 1 - (Vector3.Distance(transform.position, targetPos)/Radius);

        if(damage)
        {
            if (damageFalloff > 0.98f)
            {
                foo = 1;
            }
            else
            {
                foo = Mathf.Lerp(damageFalloff, 1, foo);
            }
        }
        else
        {
            foo = Mathf.Lerp(forceFalloff, 1, foo);
        }
        return foo;
    }
}
