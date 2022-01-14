using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private List<LifeView> lives;

    [SerializeField] int index = 0;

    [SerializeField] private int totalLivesRemaining = 3;

    public int TotalLivesRemaining { get => totalLivesRemaining; }

    public void LoseALife()
    {
        if (index >= lives.Count)
            return;

        lives[index].TriggerLoseLifeAnimation();
        index++;
        totalLivesRemaining--;
    }
}
