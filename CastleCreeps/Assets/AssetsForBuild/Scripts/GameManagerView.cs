using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerView : MonoBehaviour
{
    [SerializeField] private Abacus abacus;
    [SerializeField] private AbacusLaneDetector abacusLaneDetector;

    private int initialAbacusValue = 999;
    private int currentAbacusValue;

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
}
