using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private CameraFollow playerCamera;

    private int playerScore = 0;
    private EndState endGameState;

    public int PlayerScore
    {
        get { return playerScore; }
        set { playerScore = Math.Max(0, value); }
    }

    public EndState EndGameState
    {
        get { return endGameState; }
    }

    private void Start()
    {
        playerCamera = FindObjectOfType<CameraFollow>();
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
        endGameState = endState;
        SceneManager.LoadScene("WinOrLoss");
    }

    #endregion

    public enum EndState
    {
        GAME_LOST = 0,
        GAME_WON = 1
    }
}
