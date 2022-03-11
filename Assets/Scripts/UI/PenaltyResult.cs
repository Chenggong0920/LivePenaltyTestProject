using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PenaltyResult : MonoBehaviour
{
    [Inject]
    IUIManager uIManager;

    public void onAnimFinished()
    {
        uIManager.onPenaltyResultAnimFinished();
    }
}
