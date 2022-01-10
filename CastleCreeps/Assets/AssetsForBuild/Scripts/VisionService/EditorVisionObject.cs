using UnityEngine;
using UnityEngine.EventSystems;

public class EditorVisionObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int ObjectID;
    public bool enableDrag;

    #region WORLDSPACE_DRAG
    Vector3 prev;

    public delegate void OnDragStarted(int objectID);
    public delegate void OnDragEnded(int objectID);
    public static event OnDragStarted dragStarted;
    public static event OnDragEnded dragEnded;

    private void OnMouseDown()
    {
        if (!enableDrag) { return; }

        if (dragStarted != null)
        {
            dragStarted.Invoke(ObjectID);
        }

        prev = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        if (!enableDrag) { return; }

        var curr = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position += curr - prev;
        prev = curr;
    }

    private void OnMouseUp()
    {
        if (!enableDrag) { return; }

        if (dragEnded != null)
        {
            dragEnded.Invoke(ObjectID);
        }
    }
    #endregion


    #region CANVAS_DRAG
    Vector2 diff;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!enableDrag) { return; }

        if (dragStarted != null)
        {
            dragStarted.Invoke(ObjectID);
        }

        diff = new Vector2(transform.position.x, transform.position.y) - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!enableDrag) { return; }

        transform.position = eventData.position + diff;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!enableDrag) { return; }

        if (dragEnded != null)
        {
            dragEnded.Invoke(ObjectID);
        }
    }
    #endregion
}
