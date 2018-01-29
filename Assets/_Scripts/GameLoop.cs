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

    [SerializeField]
    private AudioSource p1Wins;

    [SerializeField]
    private AudioSource p2Wins;

    private string winnerPlayer;

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
                        CurrentState = GameState.TriggerGameOver;
                        winnerPlayer = ( ((float)(playerWorld.CurrentPlayer.PlayerID + 1) % 2) + 1 ).ToString();

                        if(playerWorld.CurrentPlayer.PlayerID == 0)
                        {
                            p1Wins.Play();
                        }
                        else
                        {
                            p2Wins.Play();
                        }

                        break;
                    }
                }
                break;

            case GameState.TriggerGameOver:
                CurrentState = GameState.GameOver;
                uiManager.SetupGameOverMessage("PLAYER " + winnerPlayer + " WINS!");
                break;
            case GameState.GameOver:
                //Poll input to reset game
                break;

        }
	}
}
