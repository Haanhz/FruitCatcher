using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class FruitFalling : MonoBehaviour
{
    public Transform fruitPrefab;// cái này để tạo instance của fruit
    //public static List<Vector3> allFruitPositions = new List<Vector3>(); // Danh sách toàn cục lưu vị trí tất cả fruits
    public List<Transform> fruitList = new List<Transform>();// cái này đc tạo ra để check fruit instance rơi ra ngoài màn hình
    private Camera cam;// cái này để check dài rộng của cam;
    System.Random rnd = new System.Random();

    public WeatherEffects weatherEffects;
    public float hurricaneSideSpeed = 2.0f;
    public float zigzagFrequency = 2.0f;                 // Frequency of zigzag motion
    public float zigzagAmplitude = 1.0f;                 // Amplitude of zigzag motion
    public GameObject basket;

    //public GameObject basket;

    // Update is called once per frame
    void Start()
    {
        cam = Camera.main;
        InvokeRepeating("Create", 3f, 2f);

    }

    void Create()
    {
        int halfHeight = Mathf.RoundToInt(cam.orthographicSize);
        int halfWidth = Mathf.RoundToInt(cam.aspect * halfHeight);

        Transform fruit = Instantiate(fruitPrefab);
        fruit.position = new Vector3(rnd.Next(-halfWidth + 5, halfWidth - 5), halfHeight + 5, 0);

        //Check if hurricane is active
        if (weatherEffects != null && weatherEffects.hurricane.activeSelf == true)
        {
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Set an initial downward velocity
                rb.velocity = new Vector2(0, -2.0f);
                StartCoroutine(ChangeDirectionRandomly(rb));
            }
        }
        fruitList.Add(fruit);

    }

    private void Update()
    {
        DestroyFruit();
    }

    IEnumerator ChangeDirectionRandomly(Rigidbody2D rb)
    {
        while (rb != null)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
            if (rb != null) // Kiểm tra nếu Rigidbody vẫn tồn tại
            {
                float randomDirection = Random.Range(-1.0f, 1.0f);
                rb.velocity = new Vector2(randomDirection * hurricaneSideSpeed, rb.velocity.y);

                // Đảm bảo fruit vẫn nằm trong giới hạn màn hình
                float halfWidth = cam.aspect * cam.orthographicSize;
                float clampedX = Mathf.Clamp(rb.position.x, -halfWidth + 1, halfWidth - 1);
                rb.position = new Vector2(clampedX, rb.position.y);
            }
        }
    }


    public void DestroyFruit()
    {
        int halfHeight = Mathf.RoundToInt(cam.orthographicSize);
        for (int i = fruitList.Count - 1; i >= 0; i--)
        {
            if (fruitList[i] != null && fruitList[i].position.y < -halfHeight)
            {
                //allFruitPositions.Remove(fruitList[i].position); // Xóa vị trí khỏi danh sách chung
                Destroy(fruitList[i].gameObject);
                fruitList.RemoveAt(i);
            }
        }
    }


}
