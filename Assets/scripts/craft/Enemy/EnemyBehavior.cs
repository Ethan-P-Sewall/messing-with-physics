using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static List<EnemyBehavior> existingEnemies { get; private set; }
    public static EnemyBehavior[] enemiesToAct { get; private set; }
    static int ActNo;

    bool isActive = false; float counter; Rigidbody RB; GameObject selectorHolder;
    [SerializeField] int moveDist; public int jumpDist { get; private set; }
    [SerializeField] CastLauncher[] casters;//orb/larm/rarm
    controllableCharacter AggroHolder;

    public static void Initialize()
    {
        existingEnemies = new List<EnemyBehavior>();
    }
    public static void EnemyTurn()
    {
        enemiesToAct = existingEnemies.ToArray();
        ActNo = -1;
        PromptNextEnemy();
    }
    static void PromptNextEnemy()
    {
        ActNo++;
        if (ActNo >= enemiesToAct.Length)
        {
            GameManager.instance.StartPlayerTurn();
        }
        else
        {
            enemiesToAct[ActNo].GetPrompted();
        }

    }

    void Start()
    {
        if (existingEnemies == null)
        {
            Initialize();
        }
        existingEnemies.Add(this);
        RB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isActive)
        {
            counter += Time.deltaTime;
            if (counter > 0.5f)
            {
                if (selectorHolder)
                {
                    if (selectorHolder.transform.childCount > 0)
                    {
                        enemySelector foo = selectorHolder.transform.GetChild(0).GetComponent<enemySelector>();
                        float bar = 999999999999;
                        for (int i = 0; i < selectorHolder.transform.childCount; i++)
                        {
                            if (Vector3.Distance(selectorHolder.transform.GetChild(i).transform.position, AggroHolder.transform.position) < bar)
                            {
                                bar = Vector3.Distance(selectorHolder.transform.GetChild(i).transform.position, AggroHolder.transform.position);
                                foo = selectorHolder.transform.GetChild(i).GetComponent<enemySelector>();
                            }
                        }
                        foo.Selected();
                    }
                    else
                    {
                        transform.position += Vector3.up * 5;
                        ImDone();
                    }
                }
                else
                {
                    Launch();
                }
            }
        }
    }

    public void Moving(Vector3 position)
    {
        transform.position = position;
        RB.velocity = Vector3.zero;
        GameManager.instance.CleanThisUp("Selector");
        Launch();
    }
    void Launch()
    {
        casters[0].Launch(AggroHolder.transform.position);

        if (PartExists(CraftComponent.ComponentPart.LArm))
        {
            casters[1].Launch(AggroHolder.transform.position);
        }
        if (PartExists(CraftComponent.ComponentPart.RArm))
        {
            casters[2].Launch(AggroHolder.transform.position);
        }
        ImDone();
    }

    //todo: check for Line of Sight
    void SpawnSelector(int amount)
    {
        GameObject foo = Instantiate(GameManager.instance.GetSelector(amount - 1), transform.position + (Vector3.up) * 6, Quaternion.identity);
        GameObject bar;
        for (int i = 0; i < foo.transform.childCount; i++)
        {
            bar = foo.transform.GetChild(i).gameObject;
            Destroy(bar.GetComponent<selector>());
            Destroy(bar.GetComponent<MeshRenderer>());
            bar.AddComponent<enemySelector>();
            bar.GetComponent<enemySelector>().SetEnemy(this);
        }
        selectorHolder = foo;
    }

    public void GetPrompted()
    {
        selectorHolder = null;
        counter = 0; isActive = true; int howManyLegs = 0;
        if (PartExists(CraftComponent.ComponentPart.LLeg))
        {
            howManyLegs++;
        }
        if (PartExists(CraftComponent.ComponentPart.RLeg))
        {
            howManyLegs++;
        }
        switch (howManyLegs)
        {
            case 1:
                SpawnSelector(1);
                break;
            case 2:
                SpawnSelector(moveDist);
                break;
        }
        AggroHolder = controllableCharacter.existingCharacters[0];//todo: change this
    }
    void ImDone()
    {
        isActive = false;
        PromptNextEnemy();
    }

    bool PartExists(CraftComponent.ComponentPart part)
    {
        bool foo = false;
        CraftComponent[] parts = GetComponentsInChildren<CraftComponent>();

        foreach (CraftComponent bar in parts)
        {
            if (bar.WhatPart() == part)
            {
                foo = true;
            }
        }

        return foo;
    }
}
