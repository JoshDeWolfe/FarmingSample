using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    public float moveSpeed = 1f;

    private Vector2 _velocity = Vector2.zero;

    private Vector2 _minBounds = Vector2.zero;
    private Vector2 _maxBounds = Vector2.zero;

    private IHasDirection dirRef = null;

    private void Start ()
    {
        dirRef = GetComponent<IHasDirection> ();
        CalculateMaxBounds ();
    }

    private void CalculateMaxBounds ()
    {
        Vector2 size = GetComponent<SpriteRenderer> ().sprite.bounds.size;

        _minBounds.x = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        _minBounds.x += size.x;
        _minBounds.y = Camera.main.transform.position.y - Camera.main.orthographicSize;
        _minBounds.y += size.x;

        _maxBounds.x = Camera.main.transform.position.x + Camera.main.orthographicSize * Screen.width / Screen.height;
        _maxBounds.x -= size.y;
        _maxBounds.y = Camera.main.transform.position.y + Camera.main.orthographicSize;
        _maxBounds.y -= size.y;
    }

    private void DetermineVelocity ()
    {
        DIRECTION dir = DIRECTION.NONE;

        if (Input.GetKey (leftKey))
        {
            _velocity.x = -moveSpeed;
            dir = DIRECTION.LEFT;
        }
        else if (Input.GetKey (rightKey))
        {
            _velocity.x = moveSpeed;
            dir = DIRECTION.RIGHT;
        }
        else
        {
            _velocity.x = 0;
        }

        if (Input.GetKey (upKey))
        {
            _velocity.y = moveSpeed;
            dir = DIRECTION.UP;
        }
        else if (Input.GetKey (downKey))
        {
            _velocity.y = -moveSpeed;
            dir = DIRECTION.DOWN;
        }
        else
        {
            _velocity.y = 0;
        }

        if (dir != DIRECTION.NONE && dirRef != null)
        {
            dirRef.SetDirection (dir);
        }
    }

    private void MoveObject ()
    {
        Vector3 offset = Vector3.zero;
        offset.x = _velocity.x;
        offset.y = _velocity.y;
        transform.position += offset * Time.smoothDeltaTime;

        RestrainPosition ();
    }

    private void RestrainPosition ()
    {
        Vector3 newPos = transform.position;

        if (transform.position.x < _minBounds.x)
        {
            newPos.x = _minBounds.x;
        }
        else if (transform.position.x > _maxBounds.x)
        {
            newPos.x = _maxBounds.x;
        }

        if (transform.position.y < _minBounds.y)
        {
            newPos.y = _minBounds.y;
        }
        else if (transform.position.y > _maxBounds.y)
        {
            newPos.y = _maxBounds.y;
        }

        transform.position = newPos;
    }

    void Update()
    {
        DetermineVelocity ();
        MoveObject ();
    }
}
