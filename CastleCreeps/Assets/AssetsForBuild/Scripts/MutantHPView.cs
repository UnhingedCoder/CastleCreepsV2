using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MutantHPView : MonoBehaviour
{
    [SerializeField] TextMeshPro hpText;

    public void SetHP(string val)
    {
        this.transform.localScale = Vector3.zero;
        this.transform.DOShakeScale(1.3f, Vector3.zero).OnComplete(() =>
        {
            this.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.25f);
        });

        hpText.text = val;
    }

    public void UpdateHP(int val)
    {
        this.transform.DOShakeScale(0.5f, new Vector3(2.0f, 2.0f, 1.0f)).OnComplete(() =>
        {
            this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        });


        hpText.text = val.ToString();
    }

    public void HideHP()
    {
        this.transform.DOShakeScale(0.25f, new Vector3(2.0f, 2.0f, 1.0f)).OnComplete(() =>
        {
            this.transform.localScale = Vector3.zero;
        });

        hpText.text = "";
    }
}
