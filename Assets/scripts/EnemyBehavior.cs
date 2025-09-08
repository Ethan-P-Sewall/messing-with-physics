using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static List<EnemyBehavior> existingEnemies { get; private set; }
    public static EnemyBehavior[] enemiesToAct { get; private set; } static int ActNo;
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
        if(ActNo >= enemiesToAct.Length)
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
        if(existingEnemies == null)
        {
            Initialize();
        }
        existingEnemies.Add(this);
    }

    public void GetPrompted()//todo: add a delay
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
        ImDone();
    }
    void ImDone()
    {
        PromptNextEnemy();
    }
}
