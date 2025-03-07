using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player playerRef;
    public GameUI gameUI;
    public System.Action onReset;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }

    public void OnScoreZoneReached(int damage)
    {
        playerRef.healthPoints -= damage;

        gameUI.UpdateScores(playerRef.healthPoints);
        gameUI.UpdateImage();

        //CheckWin();
        CheckDied();
    }

    private void CheckDied()
    {      
            if (playerRef.healthPoints <= 0)
            {
                playerRef.healthPoints = 0;                
                playerRef.isDead = true;
                //playerRef.Dead();
            }        
    }

    private void CheckWin()
    {
        /*
        if (remainingPlayers.Count == 1)
        {
            int WINNERId = players[0].isDead != true ? 1 : players[1].isDead != true ? 2 : players[2].isDead != true ? 3 : players[3].isDead != true ? 4 : 0;
            gameUI.OnGameEnds(WINNERId); //Se manda el jugador que gano
            onReset?.Invoke();
        }*/

    }

    private void OnStartGame()
    {
        //Reinicia vidas, el ScoreUI, y resetea toda la lista de RemainingPlayers
        playerRef.healthPoints = 20;
        gameUI.UpdateScores(playerRef.healthPoints);


    }



}
