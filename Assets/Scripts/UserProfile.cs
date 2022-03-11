using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserProfile
{
    ScoreCollection scoreCollection{ get; }
    void loadScore();
    void addScore(bool scored);

}

public class UserProfile : IUserProfile
{
    private ScoreCollection _scoreCollection;
    public ScoreCollection scoreCollection
    {
        get => _scoreCollection;
    }

    private const string profileKey = "DefaultUser";

    public UserProfile()
    {
        Debug.Log("UserProfile constructor");
        loadScore();
    }

    public void loadScore()
    {
        try {
            _scoreCollection = JsonUtility.FromJson<ScoreCollection>(PlayerPrefs.GetString(profileKey, ""));
        }
        catch(System.Exception e)
        {
            Debug.LogErrorFormat("Error loading score: {0}", e.Message);

            _scoreCollection = new ScoreCollection();
            _scoreCollection.scores = new List<ScoreModel>();
        }
        
        Debug.LogFormat("load finished... length: {0}", _scoreCollection.scores.Count);
    }

    public void addScore(bool scored)
    {
        _scoreCollection.scores.Add(new ScoreModel(){datetime = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), scored = scored});

        PlayerPrefs.SetString(profileKey, JsonUtility.ToJson(_scoreCollection));

        Debug.LogFormat("save finished... length: {0}", _scoreCollection.scores.Count);

    }
}


[System.Serializable]
public struct ScoreModel
{
    public string datetime;
    public bool scored;
}

[System.Serializable]
public struct ScoreCollection
{
    public List<ScoreModel> scores;
}