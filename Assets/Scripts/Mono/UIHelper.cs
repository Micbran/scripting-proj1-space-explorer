using UnityEngine;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private Text winOrLossText;
    [SerializeField] private Text scoreText;

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
    }
}
