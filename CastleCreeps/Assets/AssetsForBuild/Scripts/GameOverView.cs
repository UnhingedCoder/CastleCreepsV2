using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private GameObject wellDoneObj;
    [SerializeField] private GameObject tryAgainObj;

    public void OnLevelComplete(bool winStatus = false)
    {
        int levelIndex = LevelManager.Instance.CurrentLevel;
        levelText.text = "LEVEL " + levelIndex.ToString();


        wellDoneObj.SetActive(false);
        tryAgainObj.SetActive(false);

        if (winStatus)
            wellDoneObj.SetActive(true);
        else
            tryAgainObj.SetActive(true);
    }

    public void OnContinueClicked()
    {
        SceneHandler.Instance.LoadMapScene();
    }
}
