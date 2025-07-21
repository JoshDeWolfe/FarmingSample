using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    public float moveSpeed = 1f;

    private Vector2 _velocity = Vector2.zero;

    private void Start ()
    {
        
    }

    private void DetermineVelocity ()
    {
        if (Input.GetKey (leftKey))
        {
            _velocity.x = -moveSpeed;
        }
        else if (Input.GetKey (rightKey))
        {
            _velocity.x = moveSpeed;
        }
        else
        {
            _velocity.x = 0;
        }

        if (Input.GetKey (upKey))
        {
            _velocity.y = moveSpeed;
        }
        else if (Input.GetKey (downKey))
        {
            _velocity.y = -moveSpeed;
        }
        else
        {
            _velocity.y = 0;
        }
    }

    private void MoveObject ()
    {
        Vector3 offset = Vector3.zero;
        offset.x = _velocity.x;
        offset.y = _velocity.y;
        transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        DetermineVelocity ();
        MoveObject ();
    }
}
