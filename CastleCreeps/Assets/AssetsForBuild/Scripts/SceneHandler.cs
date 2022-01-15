using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int currectSceneIndex = scene.buildIndex;

        int nextSceneIndex = currectSceneIndex + 1;

        if (nextSceneIndex >= 2)
            nextSceneIndex = 1;

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadPrevScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int currectSceneIndex = scene.buildIndex;

        int nextSceneIndex = currectSceneIndex - 1;

        if (nextSceneIndex <= 0)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("Map");
    }

    public void LoadDemoScene()
    {
        SceneManager.LoadScene("Demo");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
