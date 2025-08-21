using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllableCharacter : MonoBehaviour
{
    public static List<controllableCharacter> existingCharacters;

    [SerializeField] GameObject selector;
    [SerializeField] GameObject projectile;
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
        SpawnSelector(new Vector3(1.5f, 6, 0));
        SpawnSelector(new Vector3(0.75f, 6, -1.5f));
        SpawnSelector(new Vector3(-1.5f, 6, 0));
        SpawnSelector(new Vector3(0.75f, 6, 1.5f));
        SpawnSelector(new Vector3(-0.75f, 6, 1.5f));
        SpawnSelector(new Vector3(-0.75f, 6, -1.5f));
    }
    void SpawnSelector(Vector3 pos)
    {
        Instantiate(selector, transform.position + pos, Quaternion.identity).GetComponent<selector>().SetCharacter(this);
    }
    public void Launch()
    {
        if(selected)
        {
            selected = false;
            GameManager.instance.CleanThisUp("Selector");
            GameObject foo = Instantiate(projectile, transform.position + (Vector3.up * 0.5f), transform.rotation);
            Vector3 v = cube.lastClickedOn.position;
            if(v.y < transform.position.y)
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
}
