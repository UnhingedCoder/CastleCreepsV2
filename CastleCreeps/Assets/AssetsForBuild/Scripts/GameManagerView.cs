using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerView : MonoBehaviour
{
    [SerializeField] private Abacus abacus;
    [SerializeField] private AbacusLaneDetector abacusLaneDetector;

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
    }

    private void OnFireTriggerPressed(int val)
    {
        CannonShootController.Instance.FireInLane(val);
    }
}
