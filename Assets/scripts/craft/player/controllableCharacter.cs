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
    public static void TriggerALaunch(Vector3 pos)
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.Launch(pos);
        }
    }
    public static void BeginTurn()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.BecomeSelectable();
        }
        unitSelect = 0;
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

    public static void NextUnit()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.Unselect();
        }
        GameManager.instance.CleanThisUp("Selector");
        if (unitSelect >= existingCharacters.Count)
        {
            unitSelect = 0;
        }
        for (int i = unitSelect; i < existingCharacters.Count; i++)
        {
            if (existingCharacters[i].selectable)
            {
                existingCharacters[i].SelectMe();
                unitSelect = i + 1;
                i = 9999;
            }
            else
            {
                unitSelect++;
            }
        }
    }
    static int unitSelect;

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
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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

    public void Launch(Vector3 pos)
    {
        if (selected)
        {
            if (!launched)
            {
                casters[0].Launch(pos);

                if (PartExists(CraftComponent.ComponentPart.LArm))
                {
                    casters[1].Launch(pos);
                }
                if (PartExists(CraftComponent.ComponentPart.RArm))
                {
                    casters[2].Launch(pos);
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
