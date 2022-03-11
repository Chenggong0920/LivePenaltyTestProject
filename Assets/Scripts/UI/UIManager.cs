using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IUIManager
{
    void drawArrow(Vector2 startPosition, Vector2 endPosition);
    void drawImpact(Vector3 position);
    void showHideArrow(bool show);
    void showHideImpact(bool show);
    void showPenaltyResult(bool scored);
    void onPenaltyResultAnimFinished();
}

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField]
    private RectTransform transformArrow;

    [SerializeField]
    private Transform transformImpact;

    [SerializeField]
    private GameObject txtGoal;

    [SerializeField]
    private GameObject txtMiss;
    private const float desiredArrowWidth = 120f;

    private float scaleRatio = 1f;

    [SerializeField]
    private GameFinished gameFinishedScreen;

    private void Awake() {
        showHideArrow(false);
        showHideImpact(false);

        txtGoal.SetActive(false);
        txtMiss.SetActive(false);

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if( canvas )
            scaleRatio = canvas.scaleFactor;
    }


    public void drawArrow(Vector2 startPosition, Vector2 endPosition)
    {
        showHideArrow(true);

        transformArrow.position = startPosition;
        
        transformArrow.sizeDelta = new Vector2(Vector2.Distance(startPosition, endPosition) / scaleRatio, desiredArrowWidth);
        transformArrow.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(endPosition.y - startPosition.y, endPosition.x - startPosition.x) * Mathf.Rad2Deg);
    }

    public void drawImpact(Vector3 position)
    {
        transformImpact.position = position;
        showHideImpact(true);
    }

    public void showHideArrow(bool show)
    {
        transformArrow.gameObject.SetActive(show);
    }

    public void showHideImpact(bool show)
    {
        transformImpact.gameObject.SetActive(show);
    }

    public void showPenaltyResult(bool scored)
    {
        try
        {
            if( scored )
                txtGoal.SetActive(true);
            else
                txtMiss.SetActive(true);
        }
        catch(System.Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void onPenaltyResultAnimFinished()
    {
        gameFinishedScreen.gameObject.SetActive(true);
        StartCoroutine(gameFinishedScreen.fadeInCoroutine());
    }
}
