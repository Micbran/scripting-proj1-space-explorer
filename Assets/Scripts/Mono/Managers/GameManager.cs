using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameManager : Manager<GameManager>
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private UITimer timerScript;

    [SerializeField] public CoinPickup.IntEvent onScoreUpdate;

    private CameraFollow playerCamera;

    private int playerScore = 0;
    private EndState endGameState;

    private float saveTime = 0;

    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = Math.Max(0, value); }
    }

    public float EndGameTime
    {
        get { return saveTime; }
        set { saveTime = value; }
    }

    public EndState EndGameState
    {
        get { return endGameState; }
    }

    private void Start()
    {
        playerCamera = FindObjectOfType<CameraFollow>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    #region Scene Control

    private void ReloadLevel()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("Level01");
    }

    #endregion


    #region Event Callbacks

    public void OnPlayerDeath()
    {
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2);

        GameObject newPlayer = Instantiate(playerPrefab, playerSpawnPoint);

        PlayerShip playerScript = newPlayer.GetComponent<PlayerShip>();
        playerScript.onPlayerDeath.AddListener(OnPlayerDeath);

        playerCamera.ChangeFollow(newPlayer.transform);

        yield break;
    }

    public void OnPlayerWinOrLoss(EndState endState)
    {
        EndGameTime = timerScript.TimerValue;
        endGameState = endState;
        switch (endGameState)
        {
            case EndState.GAME_LOST:
                SoundManager.Instance.PlaySoundEffect(SoundEffect.GameOver);
                break;
            case EndState.GAME_WON:
                SoundManager.Instance.PlaySoundEffect(SoundEffect.Win);
                break;
        }
        SceneManager.LoadScene("WinOrLoss");
    }

    public void OnCoinCollected(int pointValue)
    {
        playerScore += pointValue;
        if(onScoreUpdate != null)
        {
            onScoreUpdate.Invoke(playerScore);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name.Equals("Level01"))
        {
            timerScript = FindObjectOfType<UITimer>();
            playerSpawnPoint = GameObject.FindGameObjectWithTag("PlayerSpawnpoint").transform;
            UIScore scoreScript = FindObjectOfType<UIScore>();
            onScoreUpdate.AddListener(scoreScript.OnScoreUpdate);
            timerScript.onPlayerLoss.AddListener(OnPlayerWinOrLoss);
            WinVolume winVol = FindObjectOfType<WinVolume>();
            winVol.onPlayerWin.AddListener(OnPlayerWinOrLoss);
            playerScore = 0;
        }
    }

    #endregion

    public enum EndState
    {
        GAME_LOST = 0,
        GAME_WON = 1
    }
}
