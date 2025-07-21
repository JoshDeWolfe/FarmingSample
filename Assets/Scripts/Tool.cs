using UnityEngine;

public class Tool : MonoBehaviour
{
    public Transform originalParent;
    public TOOL toolType = TOOL.NONE;
    public SEED_TYPE seedType = SEED_TYPE.NONE;

    private void Start ()
    {
        if (originalParent == null)
        {
            originalParent = transform.parent;
        }
        ResetTool ();
    }

    public void ResetTool ()
    {
        transform.SetParent (originalParent);
        transform.localPosition = Vector3.zero;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent <Player>();

        if (p != null)
        {
            p.OnToolCollision (this);
        }
    }

}
