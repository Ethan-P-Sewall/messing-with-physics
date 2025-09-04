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
    public static void LaunchTarget()
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
            if(bar.selectable)
            {
                foo = false;
            }
        }
        return foo;
    }

    [SerializeField] SpriteRenderer myHat;//todo: hat customization
    [SerializeField] GameObject projectile;
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
                SpawnSelector(moveDist);
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
                GameObject foo = Instantiate(projectile, transform.position + (Vector3.up * 0.5f), transform.rotation);
                Vector3 v = TargetableObject.lastClickedOn.position;
                if (v.y < transform.position.y)
                {
                    v.y += 0.5f;
                }
                foo.transform.LookAt(v);
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
        if(isTurnFinished())
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
}
