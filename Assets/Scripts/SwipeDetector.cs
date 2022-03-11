using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SwipeEvent : UnityEvent<Vector2>
{
}

public class SwipeDetector : MonoBehaviour
{
    public SwipeEvent OnSwipeBegan;
    public SwipeEvent OnSwipeMoved;
    public SwipeEvent OnSwipeEnded;

    // Update is called once per frame
    
}
