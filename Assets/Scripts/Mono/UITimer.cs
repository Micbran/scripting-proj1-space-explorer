using System;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private float defaultTime;
    [SerializeField] private Text timeTextField;
    [SerializeField] public WinVolume.EndStateEvent onPlayerLoss;

    private float currentTime;

    public float TimerValue
    {
        get { return currentTime; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentTime = defaultTime;
        timeTextField.text = String.Format("{0:0.##}", defaultTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        timeTextField.text = String.Format("{0:0.##}", currentTime);
        if(currentTime <= 0)
        {
            if (onPlayerLoss != null)
            {
                onPlayerLoss.Invoke(GameManager.EndState.GAME_LOST);
            }
        }
    }
}
