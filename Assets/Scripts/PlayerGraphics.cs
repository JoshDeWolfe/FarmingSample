using UnityEngine;
using System.Collections.Generic;

public class PlayerGraphics : MonoBehaviour
{
    public bool canPlay = true;

    public SpriteRenderer spriteRenderer;
    public Sprite[] _leftSpriteSequence;
    public Sprite[] _rightSpriteSequence;
    public Sprite[] _upSpriteSequence;
    public Sprite[] _downSpriteSequence;

    public float animSpeed = 0.1f;

    private float _animTimer = 0f;
    private int _currentFrame = 0;
    private DIRECTION _currentDirection = DIRECTION.DOWN;

    private Dictionary<DIRECTION, Sprite[]> _spriteDict;

    private IHasDirection directionInterface = null;

    private void Start ()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer> ();
        }

        _spriteDict = new Dictionary<DIRECTION, Sprite[]> ();
        _spriteDict.Add (DIRECTION.LEFT, _leftSpriteSequence);
        _spriteDict.Add (DIRECTION.RIGHT, _rightSpriteSequence);
        _spriteDict.Add (DIRECTION.UP, _upSpriteSequence);
        _spriteDict.Add (DIRECTION.DOWN, _downSpriteSequence);

        directionInterface = GetComponent<IHasDirection> ();
        directionInterface?.AddDirectionListener (DirectionChanged);
    }

    private void DirectionChanged (DIRECTION newDirection)
    {
        _currentDirection = newDirection;
        UpdateDisplay ();
    }

    private void AnimationUpdate ()
    {
        _animTimer = animSpeed;
        _currentFrame++;
        if (_currentFrame >= _spriteDict[_currentDirection].Length)
        {
            _currentFrame = 0;
        }
        UpdateDisplay ();
    }

    private void UpdateDisplay ()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning ("SpriteRenderer is null!");
            return;
        }

        spriteRenderer.sprite = _spriteDict[_currentDirection][_currentFrame];
    }

    private void Update ()
    {
        if (!canPlay)
            return;

        _animTimer -= Time.deltaTime;
        if (_animTimer <= 0)
        {
            AnimationUpdate ();
        }
    }
}
