using System.Collections;
using UnityEngine;

public interface IHasDirection
{
    public DIRECTION GetDirection ();
    public void SetDirection (DIRECTION newDirection);
    public void AddDirectionListener (OnDirectionChange listener);
}
