using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IHasDirection
{
    public Transform toolParent;
    public PlayerGraphics graphics;
    public PlayerController controller;

    private Tool heldTool = null;
    private Vector3 _startPos = Vector3.zero;

    private DIRECTION _facingDirection = DIRECTION.NONE;
    OnDirectionChange directionChanged;

    private void Start ()
    {
        _startPos = transform.localPosition;
    }

    public void Reset ()
    {
        transform.localPosition = _startPos;
        ResetTool ();
    }

    public void OnGameWin ()
    {
        SetDirection (DIRECTION.DOWN);
        controller.enabled = false;
        StartCoroutine (ReactivateController());
    }

    private IEnumerator ReactivateController ()
    {
        yield return new WaitForSeconds (Global.WIN_TIME);
        controller.enabled = true;
    }

    public void ResetTool ()
    {
        if (heldTool != null)
        {
            heldTool.ResetTool ();
            heldTool = null;
        }
    }

    public Tool GetHeldTool ()
    {
        return heldTool;
    }

    public void OnToolCollision (Tool collidedTool)
    {
        if (collidedTool == null)
        {
            return;
        }

        if (heldTool != null)
        {
            heldTool.ResetTool ();
        }
        heldTool = collidedTool;
        heldTool.transform.SetParent (toolParent);
        heldTool.transform.localPosition = Vector3.zero;
    }

    public void AddDirectionListener (OnDirectionChange listener)
    {
        directionChanged += listener;
    }

    public DIRECTION GetDirection ()
    {
        return _facingDirection;
    }

    public void SetDirection (DIRECTION newDirection)
    {
        _facingDirection = newDirection;
        directionChanged?.Invoke (_facingDirection);
    }
}
