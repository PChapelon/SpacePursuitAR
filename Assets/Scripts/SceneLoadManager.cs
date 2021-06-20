using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    

    public void LoadSceneGame()
    {
        SceneManager.LoadScene("SceneAr");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadSceneMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSceneVictoryBountyHunter()
    {
        SceneManager.LoadScene("GameOverBountyHunter");
    }

    public void LoadSceneVictoryPirate()
    {
        SceneManager.LoadScene("GameOverPirate");
    }
}
