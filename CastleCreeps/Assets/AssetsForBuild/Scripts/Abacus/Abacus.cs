using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class AbacusTriggerEvent : UnityEvent<int>
{
}

public class Abacus : MonoBehaviour, IAbacusKeyParent, IAbacusTriggerParent
{
	IAbacusParent parent;

	[SerializeField] AbacusKey key5;
	[SerializeField] AbacusKey key50;
	[SerializeField] AbacusKey key500;
	[SerializeField] List<AbacusKey> key1s;
	[SerializeField] List<AbacusKey> key10s;
	[SerializeField] List<AbacusKey> key100s;
	[SerializeField] float diffActiveKeyPosition;
	[SerializeField] AbacusTrigger trigger;

	[SerializeField] float keyMoveDuration;
	[SerializeField] float nextKeyAnimDuration;

	Sequence animationSequence;

	public AbacusTriggerEvent event_AbacusTriggerPressed;

    private void Awake()
    {
		if (event_AbacusTriggerPressed == null)
			event_AbacusTriggerPressed = new AbacusTriggerEvent();
	}

    public void Init(IAbacusParent parent)
	{
		this.parent = parent;

		key5.Init(this, -diffActiveKeyPosition);
		key50.Init(this, -diffActiveKeyPosition);
		key500.Init(this, -diffActiveKeyPosition);
		foreach (var key1 in key1s) { key1.Init(this, diffActiveKeyPosition); }
		foreach (var key10 in key10s) { key10.Init(this, diffActiveKeyPosition); }
		foreach (var key100 in key100s) { key100.Init(this, diffActiveKeyPosition); }

		if (trigger != null)
		{
			trigger.Init(this);
		}
	}

	public void OnTriggerPressed()
	{
		parent.OnAbacusTriggerPressed();
		event_AbacusTriggerPressed.Invoke(GetValue());
	}

	public int GetValue()
	{
		int total = 0;

		if (key5.engaged) { total += 5; }
		if (key50.engaged) { total += 50; }
		if (key500.engaged) { total += 500; }

		foreach (var key in key1s) { total += key.engaged ? 1 : 0; }
		foreach (var key in key10s) { total += key.engaged ? 10 : 0; }
		foreach (var key in key100s) { total += key.engaged ? 100 : 0; }

		return total;
	}

	public void Animate(int total, System.Action onDone)
	{
		if (animationSequence != null)
		{
			animationSequence.Kill(false);
		}

		animationSequence = DOTween.Sequence();

		int val = total % 100 % 10;

		animationSequence.Append(
			key5.transform.DOLocalMoveY(val >= 5 ? key5.activePosition : key5.restingPosition, keyMoveDuration)
				.OnComplete(() => { key5.SetEngaged(val >= 5); }));

		if (val >= 5)
		{
			val -= 5;
			animationSequence.AppendInterval(nextKeyAnimDuration);
		}


		for (int i = 0; i < key1s.Count; i++)
		{
			var key = key1s[i];
			bool engage = i < val;

			var tween = key.transform.DOLocalMoveY(engage ? key.activePosition : key.restingPosition, keyMoveDuration)
					.OnComplete(() => { key.SetEngaged(engage); });

			if (i == 0) { animationSequence.Append(tween); } else { animationSequence.Join(tween); }
		}

		if (val != 0)
		{
			animationSequence.AppendInterval(nextKeyAnimDuration);
		}

		val = total % 100;

		animationSequence.Append(
			key50.transform.DOLocalMoveY(val >= 50 ? key50.activePosition : key50.restingPosition, keyMoveDuration)
				.OnComplete(() => { key50.SetEngaged(val >= 50); }));

		if (val >= 50)
		{
			val -= 50;
			animationSequence.AppendInterval(nextKeyAnimDuration);
		}

		for (int i = 0; i < key10s.Count; i++)
		{
			var key = key10s[i];
			bool engage = i < val / 10;

			var tween = key.transform.DOLocalMoveY(engage ? key.activePosition : key.restingPosition, keyMoveDuration)
					.OnComplete(() => { key.SetEngaged(engage); });

			if (i == 0) { animationSequence.Append(tween); } else { animationSequence.Join(tween); }
		}

		if (val != 0)
		{
			animationSequence.AppendInterval(nextKeyAnimDuration);
		}

		val = total;

		animationSequence.Append(
			key500.transform.DOLocalMoveY(val >= 500 ? key500.activePosition : key500.restingPosition, keyMoveDuration)
				.OnComplete(() => { key5.SetEngaged(val >= 500); }));

		if (val >= 500)
		{
			val -= 500;
			animationSequence.AppendInterval(nextKeyAnimDuration);
		}

		for (int i = 0; i < key100s.Count; i++)
		{
			var key = key100s[i];
			bool engage = i < val / 100;

			var tween = key.transform.DOLocalMoveY(engage ? key.activePosition : key.restingPosition, keyMoveDuration)
					.OnComplete(() => { key.SetEngaged(engage); });

			if (i == 0) { animationSequence.Append(tween); } else { animationSequence.Join(tween); }
		}

		animationSequence.AppendInterval(nextKeyAnimDuration);

		animationSequence.OnComplete(() => { onDone(); });

		animationSequence.Play();
	}

	public void KillAnimation()
	{
		if (animationSequence != null)
		{
			animationSequence.Kill(true);
			animationSequence = null;
		}
	}

	public void SetValue(int total)
	{

		key500.SetKey(total >= 500);
		if (total >= 500) { total -= 500; }
		for (int i = 0, v = total / 100; i < key100s.Count; i++) { key100s[i].SetKey(i < v); }

		total %= 100;
		key50.SetKey(total >= 50);
		if (total >= 50) { total -= 50; }
		for (int i = 0, v = total / 10; i < key10s.Count; i++) { key10s[i].SetKey(i < v); }

		total %= 10;
		key5.SetKey(total >= 5);
		if (total >= 5) { total -= 5; }
		for (int i = 0, v = total; i < key1s.Count; i++) { key1s[i].SetKey(i < v); }
	}

	public void OnKeyPressed(AbacusKey key)
	{
		if (key == key5 || key == key50 || key == key500)
		{
			parent.OnAbacusValueChanged(GetValue());
			return;
		}

		List<AbacusKey> keyArray = null;

		switch (key.Value)
		{
			case 1: keyArray = key1s; break;
			case 10: keyArray = key10s; break;
			case 100: keyArray = key100s; break;
		}

		if (keyArray == null) { return; }

		// everything before the key should be set to true, and everything after set to false
		bool toSet = true;
		for (int i = 0; i < keyArray.Count; i++)
		{
			if (keyArray[i] == key)
			{
				toSet = false;
				continue;
			}

			keyArray[i].SetKey(toSet);
		}

		parent.OnAbacusValueChanged(GetValue());
	}
}

public interface IAbacusParent
{
	void OnAbacusValueChanged(int value);
	void OnAbacusTriggerPressed();
}
