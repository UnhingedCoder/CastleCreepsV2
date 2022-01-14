using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeView : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void TriggerLoseLifeAnimation()
    {
        anim.SetTrigger("LOSELIFE");
    }
}
