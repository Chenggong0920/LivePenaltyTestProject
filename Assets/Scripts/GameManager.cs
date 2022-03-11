using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IGameManager
{
    void OnSwipeBegan(Vector2 position);
    void OnSwipeMoved(Vector2 position);
    void OnSwipeEnded(Vector2 position);
    void OnSwipeCancelled();

    void OnShootResponse(bool scored);
}

public class GameManager : MonoBehaviour, IGameManager
{
    [Inject]
    private ISoccerBall soccerBall;

    private Vector2 startPosition;

    [Inject(Id = "BallCamera")]
    private Camera ballCamera;

    [SerializeField]
    private LayerMask ballLayer;


    [SerializeField]
    private LayerMask backgroundLayer;

    

    [Inject]
    private IAPIHandler apiHandler;

    [Inject]
    private IUIManager uiManager;

    [Inject]
    private IUserProfile userProfile;

    private GameStatus gameStatus;

    public void OnSwipeBegan(Vector2 position)
    {
        if( gameStatus != GameStatus.Ready )
            return;

        if(Physics.Raycast(ballCamera.ScreenPointToRay(position), out RaycastHit hitInfo, Mathf.Infinity, ballLayer))
        {
            startPosition = position;
            gameStatus = GameStatus.Playing;
        }
    }

    public void OnSwipeMoved(Vector2 position)
    {
        if( gameStatus != GameStatus.Playing )
            return;

        uiManager.drawArrow(startPosition, position);
    }

    public void OnSwipeEnded(Vector2 position)
    {
        if( gameStatus != GameStatus.Playing )
            return;

        uiManager.showHideArrow(false);
        shoot(position);

        gameStatus = GameStatus.Finished;
    }

    public void OnSwipeCancelled()
    {
        uiManager.showHideArrow(false);
        gameStatus = GameStatus.Ready;
    }

    private void shoot(Vector2 endPosition)
    {
        if(Physics.Raycast(ballCamera.ScreenPointToRay(endPosition), out RaycastHit hitInfo, Mathf.Infinity, backgroundLayer))
        {
            Vector3 hitPosition = hitInfo.point;
            Debug.LogFormat("shoot: {0}", hitPosition);

            soccerBall.shoot(hitPosition);

            apiHandler.Shoot(endPosition);
        }
    }

    public void OnShootResponse(bool scored)
    {
        Debug.LogFormat("scored: {0}", scored);

        uiManager.showPenaltyResult(scored);

        userProfile.addScore(scored);
    }

    enum GameStatus
    {
        Ready,
        Playing,
        Finished
    }
}
