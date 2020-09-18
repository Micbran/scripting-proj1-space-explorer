using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] private Text scoreField;

    public void OnScoreUpdate(int newScore)
    {
        scoreField.text = newScore.ToString();
    }
}
