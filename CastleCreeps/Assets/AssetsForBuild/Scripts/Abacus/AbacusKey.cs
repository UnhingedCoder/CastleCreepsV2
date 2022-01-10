using UnityEngine;

public class AbacusKey : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] public bool engaged;

    public float restingPosition;
    public float activePosition;

    public int Value { get { return value; } }

    IAbacusKeyParent parent;

    public void Init(IAbacusKeyParent parent, float diffActivePosition)
    {
        this.parent = parent;
        restingPosition = transform.localPosition.y;
        activePosition = restingPosition + diffActivePosition;
        SetKey(engaged);
    }

    public void ToggleKey()
    {
        SetKey(!engaged);
        parent.OnKeyPressed(this);
    }

    public void Reset()
    {
        SetKey(false);
    }

    public void SetKey(bool active)
    {
        engaged = active;
        float posY = engaged ? activePosition : restingPosition;
        transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);
    }

    // this is in cases where position is controlled by parent
    // example: animating to a value
    public void SetEngaged(bool active)
    {
        engaged = active;
    }
}

public interface IAbacusKeyParent
{
    void OnKeyPressed(AbacusKey key);
}

