using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusVisionCanvasView : MonoBehaviour, IAbacusParent
{
	[SerializeField] Abacus abacus;

	private void Awake()
	{
		abacus.Init(this);
	}

    private void OnEnable()
    {
		ToggleAbacus();
    }

    public void OnAbacusValueChanged(int value)
	{
		// not required to implement
	}

	public void OnAbacusTriggerPressed()
	{
		// not required to implement
	}

	[ContextMenu("GET ABACUS VALUE")]
	public void GetCurrentAbacusValue()
	{
		Debug.Log("CURRENT VALUE IS: " + abacus.GetValue());
	}

	public void ToggleAbacus()
	{
		if (abacus.gameObject.activeInHierarchy)
		{
			abacus.gameObject.SetActive(false);
		}
		else
		{
			abacus.gameObject.SetActive(true);
		}
	}
}
