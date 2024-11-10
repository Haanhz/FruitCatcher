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

    private void Start() {
        text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<SpriteRenderer>().color == basket.GetComponent<SpriteRenderer>().color){
            Debug.Log("You failed!");
            basket.GetComponent<Moving>().loose = true;
            text.SetActive(true);
            Invoke("Load",2f);
            
        }
    }

    void Load(){
        SceneManager.LoadScene(0);
    }
}
