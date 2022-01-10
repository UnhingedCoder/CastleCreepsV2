using System.Collections;
using UnityEngine;

public class AbacusTrigger : MonoBehaviour
{
    [SerializeField] EditorVisionObject trigger;
    [SerializeField] GameObject triggerPressed;
    [SerializeField] float triggerUnblockDelay;

    IAbacusTriggerParent parent;
    bool isTriggerPressed;

    public void Init(IAbacusTriggerParent parent)
    {
        this.parent = parent;
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        SetTriggerVisibility(true);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        PostUnblockTrigger();
    }

    void SetTriggerVisibility(bool visible)
    {
        trigger.gameObject.SetActive(visible);
        triggerPressed.SetActive(!visible);
    }

    public void PressTrigger()
    {
        if (isTriggerPressed)
        {
            return;
        }


        Debug.Log("ABACUS TRIGGER PRESSED");

        isTriggerPressed = true;
        parent.OnTriggerPressed();
        SetTriggerVisibility(false);

        StartCoroutine(UnblockTrigger(triggerUnblockDelay));
    }

    IEnumerator UnblockTrigger(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        PostUnblockTrigger();
    }

    void PostUnblockTrigger()
    {
        isTriggerPressed = false;
        SetTriggerVisibility(true);
    }
}

public interface IAbacusTriggerParent
{
    void OnTriggerPressed();
}

