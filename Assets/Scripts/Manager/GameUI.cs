using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreText1;
    [SerializeField] private Player playerRef;
    [SerializeField] private Image playerImage;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private TextMeshProUGUI winText;

    public System.Action onStartGame;




    public void UpdateScores(int scorePlayer1)
    {
        scoreText1.SetScore(scorePlayer1);

    }

    public void UpdateImage()
    {
        if (playerRef.healthPoints <= 10)
        {
            playerImage.sprite = playerRef.sadFace;
        }        
    }

    public void ResetImage()
    {
        playerImage.sprite = playerRef.happyFace;;
    }

    //SI le da clcik el evento onStartGame se invoca
    public void OnStartGameButtonClicked()
    {
        menuCanvas.enabled = false;
        ResetImage();
        onStartGame?.Invoke();
    }

    public void OnGameFailure()
    {
        menuCanvas.enabled = true;
        winText.text = " THE ZOMBIES ATE YOUR BRAINS";
    }

    public void OnGameEnds()
    {
        menuCanvas.enabled = true;
        winText.text = "Player survived another day CONGRATULATIONS";
    }

}
