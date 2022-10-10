using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("1_MainMenu"); // 0
    }

    public void LoadGame()
    {
        ScoreKeeper.GetInstance().ResetScore();
        SceneManager.LoadScene("2_Game"); // 1
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("3_GameOver", sceneLoadDelay)); // 2
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit game...");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
