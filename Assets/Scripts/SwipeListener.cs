using UnityEngine;
using Zenject;

public class SwipeListener : MonoBehaviour
{
    [Inject]
    private IGameManager m_gameManager;
    

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                m_gameManager.OnSwipeBegan(touch.position);
            }

            //Detects Swipe while finger is still moving
            else if (touch.phase == TouchPhase.Moved)
            {
                m_gameManager.OnSwipeMoved(touch.position);
            }

            //Detects swipe after finger is released
            else if (touch.phase == TouchPhase.Ended)
            {
                m_gameManager.OnSwipeEnded(touch.position);
            }

            else if (touch.phase == TouchPhase.Canceled )
            {
                m_gameManager.OnSwipeCancelled();   
            }
        }
    }
}
