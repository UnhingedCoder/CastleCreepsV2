using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CannonAbacusDisplayView : MonoBehaviour
{

    [SerializeField] Animator anim;

    [SerializeField] TextMeshPro initValueText;
    [SerializeField] TextMeshPro incrementedValueText;
    [SerializeField] TextMeshPro decrementedValueText;

    IEnumerator numberScrollCo;

    public void SetCannonAbacusValue(int newValue, int oldValue)
    {
        initValueText.text = oldValue.ToString();
        incrementedValueText.text = newValue.ToString();
        decrementedValueText.text = newValue.ToString();

        if (numberScrollCo != null)
        {
            StopCoroutine(numberScrollCo);
            ResetOverheadAbacusValue();
        }
        numberScrollCo = NumberScroll(newValue, oldValue);
        StartCoroutine(numberScrollCo);
    }

    private void ResetOverheadAbacusValue()
    {
        anim.gameObject.SetActive(false);
        anim.gameObject.SetActive(true);
    }

    private IEnumerator NumberScroll(int newValue, int oldValue)
    {
        if (oldValue < newValue)
            anim.SetTrigger("INCREMENT");
        else if (oldValue > newValue)
            anim.SetTrigger("DECREMENT");
        else
            yield return null;



        yield return new WaitForSeconds(0.7f);
        initValueText.text = newValue.ToString();
    }
}
