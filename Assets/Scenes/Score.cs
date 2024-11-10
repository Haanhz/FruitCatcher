using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{   
    public float score=0f;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<SpriteRenderer>().color== GetComponent<SpriteRenderer>().color && GetComponent<Moving>().loose==false){
            score+=10f;
        }
        else score-=10f;
        Debug.Log("Score:"+score);
    }
}
