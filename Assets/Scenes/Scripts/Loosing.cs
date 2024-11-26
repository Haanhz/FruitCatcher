using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loosing : MonoBehaviour
{
    public GameObject text;
    public GameObject basket;
    private float time;

    private void Start() {
        text.SetActive(false);
       
    }

    private void OnTriggerEnter2D(Collider2D other) {
        time =basket.GetComponent<Catching>().timeCount;
        if((other.GetComponent<SpriteRenderer>().color == basket.GetComponent<SpriteRenderer>().color) && time>1f ){
            Debug.Log("You failed!");
            basket.GetComponent<Moving>().loose = true;
            text.SetActive(true);
            Invoke("Load",2f);
            
        }
    }

    void Load(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
