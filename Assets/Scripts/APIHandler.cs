using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Zenject;

public interface IAPIHandler
{
    void Shoot(Vector2 position);
}

public class APIHandler : IAPIHandler
{
    private const string ServerDomain = "https://94e554b0-553b-4040-9e9b-a6cfc3d20450.mock.pstmn.io";
    private const float radius = 88.9f;

    // public delegate void ShootResponseHandler(bool scored);
    // public ShootResponseHandler OnShootResponse;
    private const int DelayBeforeShootRequest = 500;

    [Inject]
    private IGameManager gameManager;


    public enum RequestType
    {
        Shoot,
    }


    public void Shoot(Vector2 position)
    {
        UnityWebRequest request = UnityWebRequest.Put(ServerDomain + "/shoot", JsonUtility.ToJson(new ShootRequest(){x = position.x, y = position.y, radius = radius}));

        // StartCoroutine(HandleRequest(request, RequestType.Shoot));
        HandleRequest(request, RequestType.Shoot, 500);
    }

    private async void HandleRequest(UnityWebRequest www, RequestType requestType, int delayBeforeRequest = 0)
    {
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        www.method = UnityWebRequest.kHttpVerbPOST;

        await Task.Delay(delayBeforeRequest);

        var operation = www.SendWebRequest();
        while(!operation.isDone)
            await Task.Yield();

        if( www.result != UnityWebRequest.Result.Success )
        {
            Debug.LogErrorFormat("Error: {0}", www.error);
        }
        else
        {
            switch(requestType)
            {
                case RequestType.Shoot:
                {
                    ShootResponse shootResponse = JsonUtility.FromJson<ShootResponse>(www.downloadHandler.text);
                    gameManager.OnShootResponse(shootResponse.scored);

                    break;
                }
            }
        }
    }

    struct ShootRequest
    {
        public float x;
        public float y;
        public float radius;
    }

    struct ShootResponse
    {
        public bool scored;
    }
}
