using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private List<LevelTokenView> levelTokens;


    private void OnEnable()
    {
        SetLevelTokens();    
    }

    private void SetLevelTokens()
    {
        for (int i = 0; i < levelTokens.Count; i++)
        {
            int lvl = i + 1;
            levelTokens[i].Init(lvl, LEVELSTATE.COMPLETED);
        }
    }
}
