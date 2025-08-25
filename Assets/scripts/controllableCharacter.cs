using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllableCharacter : MonoBehaviour
{
    public static List<controllableCharacter> existingCharacters;

    [SerializeField] GameObject projectile;
    [SerializeField] int moveDist = 1;
    public int jumpDist { get; private set; } = 3;
    bool selected = false;

    void Start()
    {
        if (existingCharacters == null)
        {
            existingCharacters = new List<controllableCharacter>();
        }
        existingCharacters.Add(this);
    }

    void OnMouseUpAsButton()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.Unselect();
        }
        selected = true;
        GameManager.instance.CleanThisUp("Selector");
        SpawnSelector(moveDist);
    }

    public void Launch()
    {
        if (selected)
        {
            selected = false;
            GameManager.instance.CleanThisUp("Selector");
            GameObject foo = Instantiate(projectile, transform.position + (Vector3.up * 0.5f), transform.rotation);
            Vector3 v = cube.lastClickedOn.position;
            if (v.y < transform.position.y)
            {
                v.y += 0.5f;
            }
            foo.transform.LookAt(v);
        }
    }
    public void Unselect()
    {
        selected = false;
    }

    public static void TargetBlock()
    {
        foreach (controllableCharacter foo in existingCharacters)
        {
            foo.Launch();
        }
    }

    void SpawnSelector(int amount)
    {
        GameObject foo = Instantiate(GameManager.instance.GetSelector(amount-1), transform.position + (Vector3.up)*6, Quaternion.identity);
        for (int i = 0; i < foo.transform.childCount; i++)
        {
            foo.transform.GetChild(i).gameObject.GetComponent<selector>().SetCharacter(this);
        }
    }
}
