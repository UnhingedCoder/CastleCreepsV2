using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbacusVisionCanvasView : MonoBehaviour, IAbacusParent
{
	[SerializeField] Abacus abacus;

	private void Start()
	{
		abacus.Init(this);
	}

	public void OnAbacusValueChanged(int value)
	{
		// not required to implement
	}

	public void OnAbacusTriggerPressed()
	{
		// not required to implement
	}
}
