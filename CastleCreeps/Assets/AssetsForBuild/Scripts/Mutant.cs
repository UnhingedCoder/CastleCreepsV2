using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{

    private int health;

    public int Health { get => health; }

    private MutantView mView;

    private MutantHPView mhealthView;

    private int totalAttemptsToHeal;

    private void Awake()
    {
        mView = this.GetComponent<MutantView>();
        mhealthView = this.GetComponentInChildren<MutantHPView>();
    }

    public void Init(Transform destPos, float speed, HPAttributes hp)
    {
        mView.Init(destPos, speed);
        health = hp.hPValue;
        mhealthView.SetHP(hp.hPString);
    }

    public void OnTakingHit(int val)
    {
        int updatedHP = 0;

        updatedHP = Mathf.Abs(health - val);

        health = updatedHP;

        if (health == 0)
        {
            mView.Heal();
            mhealthView.HideHP();
        }
        else
        {
            mView.TakeAHit();
            mhealthView.UpdateHP(health);
        }

        totalAttemptsToHeal++;

    }
}
