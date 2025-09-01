using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] selectors;

    public enum GameState {Outside, Player, Enemy}
    public GameState currentState { get; private set; } = GameState.Outside;

    void Start()
    {
        instance = this;
        StartTurn();//debug
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

    public void StartTurn()
    {
        currentState = GameState.Player;
        controllableCharacter.BeginTurn();
    }

    public void EndTurn()
    {
        currentState = GameState.Enemy;
    }
}
