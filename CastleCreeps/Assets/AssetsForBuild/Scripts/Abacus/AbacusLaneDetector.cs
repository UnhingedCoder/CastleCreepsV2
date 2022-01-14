using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbacusLaneDetector : MonoBehaviour
{
    private int laneIndex = -1;

    private float rightLaneThreshold;
    private float leftLaneThreshold;

    private RectTransform rectTransform;

    public int LaneIndex { get => laneIndex; }

    private void Awake()
    {
        rightLaneThreshold = (Screen.width / 3) / 2;
        leftLaneThreshold = rightLaneThreshold * -1f;

        rectTransform = this.GetComponent<RectTransform>();
    }

    private void Start()
    {
        InvokeRepeating("DetectAbacusLane", 0.3f, 0.3f);
    }

    private void DetectAbacusLane()
    {
        //Left lane
        if (this.rectTransform.anchoredPosition.x < leftLaneThreshold && this.rectTransform.anchoredPosition.x < rightLaneThreshold)
        {
            laneIndex = 0;
        }
        //Middle lane
        else if (this.rectTransform.anchoredPosition.x > leftLaneThreshold && this.rectTransform.anchoredPosition.x < rightLaneThreshold)
        {
            laneIndex = 1;
        }
        //Right lane
        else if (this.rectTransform.anchoredPosition.x > rightLaneThreshold && this.rectTransform.anchoredPosition.x > leftLaneThreshold)
        {
            laneIndex = 2;
        }
        else
        {
            laneIndex = -1;
        }
    }
}
