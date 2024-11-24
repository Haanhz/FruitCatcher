using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{   
    
    //public static int scoreValue;
    public GameObject basket;

    public TMP_Text score;

    // void Start()
    // {
    //     basket.GetComponent<Score>().score;
    // }

    void Update()
    {
        score.text = ""+ basket.GetComponent<Score>().score.ToString();
    }
    
}
