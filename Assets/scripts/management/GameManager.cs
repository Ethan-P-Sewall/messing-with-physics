using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject[] selectors;
    [SerializeField] Image canvasImage;
    [SerializeField] Sprite[] images;

    public enum GameState {Outside, Player, Enemy}
    public GameState currentState { get; private set; } = GameState.Outside;
    bool enemyQueued, playerQueued;

    void Start()
    {

        Application.targetFrameRate = 60;
        instance = this;
        StartPlayerTurn();//todo: wrap this into the game loop
    }

    void Update()
    {
        if (currentState == GameState.Player)
        {
            if (Input.GetButtonDown("Next Unit"))
            {
                controllableCharacter.NextUnit();
            }
        }

        if (enemyQueued)
        {
            if (GameObject.FindGameObjectsWithTag("PlayerCast").Length < 1)
            {
                currentState = GameState.Enemy;
                canvasImage.sprite = images[1];
                EnemyBehavior.EnemyTurn();
                enemyQueued = false;
            }
        }
        else if (playerQueued)
        {
            if (GameObject.FindGameObjectsWithTag("EnemyCast").Length < 1)
            {
                currentState = GameState.Player;
                canvasImage.sprite = images[0];
                controllableCharacter.BeginTurn();
                playerQueued = false;
            }
        }
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

    public void StartPlayerTurn()
    {
        currentState = GameState.Player;
        controllableCharacter.BeginTurn();
        canvasImage.sprite = images[0];
    }

    public void EndPlayerTurn()
    {
        enemyQueued = true;
    }
}
