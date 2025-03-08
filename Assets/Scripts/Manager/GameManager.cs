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
    public System.Action onStartGame;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            onStartGame += OnStartGame;
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
                gameUI.OnGameFailure(); //Se manda el jugador que gano
                onReset?.Invoke();
            }        
    }

    public void Win()
    {
       gameUI.OnGameEnds();
        onReset?.Invoke();

    }

    private void OnStartGame()
    {
        //Reinicia vidas, el ScoreUI, y resetea toda la lista de RemainingPlayers
        playerRef.healthPoints = playerRef.maxHealthPoints;
        gameUI.UpdateScores(playerRef.healthPoints);


    }



}
