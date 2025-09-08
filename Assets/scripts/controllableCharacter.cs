using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllableCharacter : MonoBehaviour
{
    public static List<controllableCharacter> existingCharacters { get; private set; }
    public static void Initialize()
    {
        existingCharacters = new List<controllableCharacter>();
    }
    public static void TriggerALaunch()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.Launch();
        }
    }
    public static void BeginTurn()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.BecomeSelectable();
        }
    }
    public static bool isTurnFinished()
    {
        bool foo = true;
        foreach (controllableCharacter bar in existingCharacters)
        {
            if (bar.selectable)
            {
                foo = false;
            }
        }
        return foo;
    }
    //todo:scroll through units
    public static void NextUnit()
    {
        for (int i = 0; i < existingCharacters.Count; i++)
        {
            if(existingCharacters[i].selectable)
            {
                existingCharacters[i].SelectMe();
                i = 9999;
            }
        }
    }

    [SerializeField] SpriteRenderer myHat;//todo: hat customization
    [SerializeField] CastLauncher[] casters;//orb/larm/rarm
    [SerializeField] int moveDist = 1;
    public int jumpDist { get; private set; } = 3;
    public bool selectable { get; private set; } = false;
    bool moved; bool launched; bool selected = false;

    void Start()
    {
        if (existingCharacters == null)
        {
            Initialize();
        }
        existingCharacters.Add(this);
    }

    void OnMouseUpAsButton()
    {
        SelectMe();
    }

    public void SelectMe()
    {
        if (selectable)
        {
            foreach (controllableCharacter foo in existingCharacters)
            {
                foo.Unselect();
            }
            selected = true;
            myHat.transform.localScale = new Vector3(2.5f, 2.5f, myHat.transform.localScale.z);
            GameManager.instance.CleanThisUp("Selector");
            if (!moved)
            {
                int howManyLegs = 0;

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
                    case 0:
                        moved = true;
                        break;
                    case 1:
                        SpawnSelector(1);
                        break;
                    case 2:
                        SpawnSelector(moveDist);
                        break;
                }

            }
        }
    }

    public void Moving(Vector3 position)
    {
        transform.position = position;
        GameManager.instance.CleanThisUp("Selector");
        moved = true;
        if (launched && moved)
        {
            selected = false;
            FinishedMyTurn();
        }
    }

    public void Launch()
    {
        if (selected)
        {
            if (!launched)
            {
                casters[0].Launch();

                if (PartExists(CraftComponent.ComponentPart.LArm))
                {
                    casters[1].Launch();
                }
                if (PartExists(CraftComponent.ComponentPart.RArm))
                {
                    casters[2].Launch();
                }
            }

            launched = true;
            if (launched && moved)
            {
                selected = false;
                FinishedMyTurn();
            }

        }
    }

    public void Unselect()
    {
        selected = false;
        myHat.transform.localScale = new Vector3(1.4f, 1.4f, myHat.transform.localScale.z);
    }

    public void BecomeSelectable()
    {
        myHat.color = Color.blue;
        selectable = true;
        moved = false;
        launched = false;
    }

    public void FinishedMyTurn()
    {
        Unselect();
        GameManager.instance.CleanThisUp("Selector");
        selectable = false;
        myHat.color = Color.white;
        if (isTurnFinished())
        {
            GameManager.instance.EndPlayerTurn();
        }
    }

    void SpawnSelector(int amount)
    {
        GameObject foo = Instantiate(GameManager.instance.GetSelector(amount - 1), transform.position + (Vector3.up) * 6, Quaternion.identity);
        for (int i = 0; i < foo.transform.childCount; i++)
        {
            foo.transform.GetChild(i).gameObject.GetComponent<selector>().SetCharacter(this);
        }
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

    public void OrbFail()
    {
        CraftComponent[] parts = GetComponentsInChildren<CraftComponent>();

        foreach (CraftComponent bar in parts)
        {
            bar.TakeDamage(9999);
        }
        existingCharacters.Remove(this);
        if (existingCharacters.Count == 0)
        {
            //game over logic;
        }
        Destroy(gameObject);
    }
}
