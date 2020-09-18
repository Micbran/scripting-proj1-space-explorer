using System;
using UnityEngine;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private Text winOrLossText;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeLeftText;
    [SerializeField] private Text timeBonusText;
    [SerializeField] private Text totalScoreText;

    private void Awake()
    {
        if (GameManager.Instance.EndGameState == GameManager.EndState.GAME_LOST)
        {
            winOrLossText.text = "You lost...";
        }
        else
        {
            winOrLossText.text = "You win!!!";
        }
        scoreText.text = GameManager.Instance.PlayerScore.ToString();
        timeLeftText.text = String.Format("{0:0.##}", GameManager.Instance.EndGameTime);
        int temp = (int)GameManager.Instance.EndGameTime * 10;
        timeBonusText.text = temp.ToString();
        temp += GameManager.Instance.PlayerScore;
        totalScoreText.text = temp.ToString();
    }
}
