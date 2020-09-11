using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private CameraFollow playerCamera;

    private void Awake()
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

    private void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

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
}
