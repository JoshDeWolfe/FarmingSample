using UnityEngine;

public class Player : MonoBehaviour
{
    public DIRECTION facingDirection = DIRECTION.NONE;
    public Transform toolParent;
    private Tool heldTool = null;

    public void Reset ()
    {
        transform.localPosition = Vector3.zero;
        ResetTool ();
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

    public Tool GetHeldTool ()
    {
        return heldTool;
    }

    public void ResetTool ()
    {
        if (heldTool != null)
        {
            heldTool.ResetTool ();
            heldTool = null;
        }
    }
}
