using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            ReloadLevel();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
}
