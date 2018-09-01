using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlPoint : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public uint Id;
    public Point Point;
    public Sprite LockedArrowSprite;

    Vector3 startPosition;
    Vector3 offsetToMouse;
    float zDistanceToCamera;

    private bool locked = false;
    private bool lockedOnX = false;
    private bool lockedOnY = false;

    private void Awake()
    {
        Point = new Point(Id, transform.position.x, transform.position.y);
    }

    public void LockOnX()
    {
        lockedOnX = true;
    }
    
    public void LockOnY()
    {
        lockedOnY = true;
    }

    public void Lock()
    {
        locked = true;
        Point.position = transform.position;
        GetComponent<SpriteRenderer>().sprite = LockedArrowSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (locked || (lockedOnX && lockedOnY)) return;

        startPosition = transform.position;
        zDistanceToCamera = Mathf.Abs(startPosition.z - Camera.main.transform.position.z);

        offsetToMouse = startPosition - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (locked || (lockedOnX && lockedOnY)) return;

        if (Input.touchCount > 1)
            return;

        var newPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDistanceToCamera)
            ) + offsetToMouse;
        newPos = transform.parent.worldToLocalMatrix.MultiplyPoint3x4(newPos);

        if (lockedOnX) transform.localPosition = new Vector3(transform.localPosition.x, newPos.y, newPos.z);
        else if (lockedOnY) transform.localPosition = new Vector3(newPos.x, transform.localPosition.y, newPos.z);
        else transform.localPosition = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (locked || (lockedOnX && lockedOnY)) return;

        offsetToMouse = Vector3.zero;
    }
}
