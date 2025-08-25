using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] selectors;

    void Start()
    {
        instance = this;   
    }

    public void CleanThisUp(string _this_)
    {
        GameObject[] foo = GameObject.FindGameObjectsWithTag(_this_);
        for (int i = 0; i < foo.Length; i++)
        {
            Destroy(foo[i]);
        }
    }

    public GameObject GetSelector(int index)
    {
        return selectors[index];
    }
}
