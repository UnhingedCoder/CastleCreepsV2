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

    public void Init(int levelIndex, LEVELSTATE state)
    {
        baseLevelText.text = levelIndex.ToString();
        pinLevelText.text = levelIndex.ToString();

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
    }
}
