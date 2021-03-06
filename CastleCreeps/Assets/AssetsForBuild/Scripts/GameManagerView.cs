using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerView : MonoBehaviour
{
    [SerializeField] private Abacus abacus;
    [SerializeField] private AbacusLaneDetector abacusLaneDetector;

    [SerializeField] private GameOverView gameOverScreen;

    private int initialAbacusValue = 999;
    private int currentAbacusValue;

    [SerializeField] private bool isGamePaused = false;

    public bool IsGamePaused { get => isGamePaused; }

    private void Awake()
    {
    }

    private void Start()
    {
        abacus.event_AbacusTriggerPressed.AddListener(OnFireTriggerPressed);
    }

    private void Update()
    {
        //Set the position of cannon
        CannonShootController.Instance.SetCannonPosition(abacusLaneDetector.LaneIndex);

        currentAbacusValue = abacus.GetValue();

        if (initialAbacusValue != currentAbacusValue)
        {
            CannonShootController.Instance.SetCannonOverheadAbacusValue(currentAbacusValue, initialAbacusValue);
            initialAbacusValue = currentAbacusValue;
        }
    }

    private void OnFireTriggerPressed(int val)
    {
        CannonShootController.Instance.FireInLane(val);
    }

    public void OnGameOver(bool status)
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.OnLevelComplete(status);
    }
}
