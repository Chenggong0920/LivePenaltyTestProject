using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UserProfileScreen : MonoBehaviour
{
    [Inject]
    private IUserProfile userProfile;

    [SerializeField]
    private Transform contentTransform;

    [SerializeField]
    private GameObject noDataPrefab;

    [SerializeField]
    private ScoreItemUI scoreItemUIPrefab;

    private void Start() 
    {
        updateUserProfile();
    }

    private void updateUserProfile()
    {
        if( userProfile.scoreCollection.scores == null || userProfile.scoreCollection.scores.Count == 0 )
            Instantiate(noDataPrefab, contentTransform);
        else
        {
            foreach(ScoreModel scoreModel in userProfile.scoreCollection.scores)
            {
                Instantiate(scoreItemUIPrefab, contentTransform).init(scoreModel.datetime, scoreModel.scored);
            }
        }
    }
}
