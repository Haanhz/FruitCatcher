using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard2 : MonoBehaviour
{   
    
    //public static int scoreValue;
    public GameObject wind;

    public TMP_Text score;

    // void Start()
    // {
    //     basket.GetComponent<Score>().score;
    // }

    void Update()
    {
        score.text = ""+ wind.GetComponent<WindEffect>().score.ToString();
    }
    
}
