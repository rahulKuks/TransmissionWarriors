using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public enum GameState
    {
        StartIdle,
        Initialize,
        Battle,
        TriggerGameOver,
        GameOver
    }

    public GameState CurrentState { set; get; }

    [SerializeField]
    private PlayerWorld[] playerWorlds;

    [SerializeField]
    private UIManager uiManager;

	void Start ()
    {
        CurrentState = GameState.Initialize;
    }
	

	void Update ()
    {
		switch(CurrentState)
        {
            case GameState.Initialize:
                foreach (PlayerWorld playerWorld in playerWorlds)
                {
                    playerWorld.EnemySpawner.Initialize();
                    CurrentState = GameState.Battle;
                }
                break;

            case GameState.Battle:
                foreach (PlayerWorld playerWorld in playerWorlds)
                {
                    if(playerWorld.CurrentPlayer.CurrentState == PlayerControl.PlayerState.Dead)
                    {
                        CurrentState = GameState.GameOver;
                    }
                }
                break;

            case GameState.TriggerGameOver:
                uiManager.SetGameOverMessage();
                break;
            case GameState.GameOver:
                //Poll input to reset game
                break;

        }
	}
}
