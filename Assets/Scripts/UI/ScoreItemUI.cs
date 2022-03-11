using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreItemUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtDatetime;
    
    [SerializeField]
    private TextMeshProUGUI txtScored;

    public void init(string datetime, bool scored)
    {
        txtDatetime.text = datetime;
        txtScored.text = scored? "Goal": "Miss";
    }
}
