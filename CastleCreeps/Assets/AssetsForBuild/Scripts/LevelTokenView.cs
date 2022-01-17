using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum LEVELSTATE
{
    LOCKED,
    COMPLETED,
    SELECTED
}

public class LevelTokenView : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private TextMeshProUGUI baseLevelText;
    [SerializeField] private TextMeshProUGUI pinLevelText;

    int _levelIndex;

    public void Init(int levelIndex, LEVELSTATE state)
    {
        _levelIndex = levelIndex;
        baseLevelText.text = _levelIndex.ToString();
        pinLevelText.text = _levelIndex.ToString();

        switch (state)
        {
            case LEVELSTATE.LOCKED:
                {
                    anim.SetBool("COMPLETED", false);
                    anim.SetBool("SELECTED", false);
                }
                break;

            case LEVELSTATE.COMPLETED:
                {
                    anim.SetBool("COMPLETED", true);
                    anim.SetBool("SELECTED", false);
                }
                break;

            case LEVELSTATE.SELECTED:
                {
                    anim.SetBool("COMPLETED", true);
                    anim.SetBool("SELECTED", true);
                }
                break;
        }
    }

    public void OnLevelClicked()
    {
        LevelManager.Instance.SetCurrentLevel(_levelIndex);
        SceneHandler.Instance.LoadDemoScene();
    }
}
