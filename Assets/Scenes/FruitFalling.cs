using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class FruitFalling : MonoBehaviour
{   
    public Transform fruitPrefab;// cái này để tạo instance của fruit
    public List<Transform> fruitList = new List<Transform>();// cái này đc tạo ra để check fruit instance rơi ra ngoài màn hình
    private Camera cam;// cái này để check dài rộng của cam;
    System.Random rnd = new System.Random();

    //public GameObject basket;

    // Update is called once per frame
    void Start()
    {   
        cam = Camera.main;
        InvokeRepeating("Create",3f, 2f);

    }

    void Create(){
        int halfHeight = Mathf.RoundToInt(cam.orthographicSize);
        int halfWidth = Mathf.RoundToInt(cam.aspect * halfHeight);
 
        Transform fruit = Instantiate(fruitPrefab);
        fruit.position = new Vector3(rnd.Next(-halfWidth+2,halfWidth-2), halfHeight +2 , 0);
        fruitList.Add(fruit);

    }

    private void Update() {
        DestroyFruit();
    }

    public void DestroyFruit(){
        int halfHeight = Mathf.RoundToInt(cam.orthographicSize);
        for(int i= fruitList.Count -1; i>=0; i--){
            if(fruitList[i] != null && fruitList[i].position.y < -halfHeight ){
                Destroy(fruitList[i].gameObject);
                fruitList.RemoveAt(i);
            }

        }
    }

}
