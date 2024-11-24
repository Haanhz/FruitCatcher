using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Color32 white = new Color32(1,1,1,1);
    public Color32 red = new Color32(1,0,0,1);
    private List<Color32> basketColor = new List<Color32>();
    SpriteRenderer spriteRenderer;
    private int colorIndex = 0;

    private void Start() {
        basketColor.Add(white);
        basketColor.Add(red);
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColor", 0f, 10f);
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.tag=="Fruit"){
    //     Destroy(other.gameObject);
    //     }   
    // }

    //hàm tự đổi màu sau n giây
    private void ChangeColor(){
        spriteRenderer.color = basketColor[colorIndex];
        colorIndex = (colorIndex+1) % basketColor.Count; //cần xem thêm
    }
}
