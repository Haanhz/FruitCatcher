using UnityEngine;

public class WindEffect : MonoBehaviour {
    public float moveSpeed = 5f;        // Tốc độ di chuyển của luồng gió
    public float windStrength = 10f;   // Độ mạnh của gió

    public float score=0f;
    

    void Update() {
        // Di chuyển luồng gió sang bên trái (trục X âm)
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // void OnCollisionEnter2D(Collision2D other) {
    //     Destroy(other.gameObject);        
    // }
    private void OnCollisionEnter2D(Collision2D other)
{
    score+=10f;
    // Lấy Rigidbody2D từ đối tượng va chạm
    Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();

    if (rb != null)
    {
        // Tạo lực gió tác động theo trục X (Vector3.left)
        Vector2 windForce = Vector2.left * windStrength;

        // Áp lực gió lên Rigidbody2D
        rb.AddForce(windForce, ForceMode2D.Impulse);
        
    }
    
    Debug.Log("Hit"+score);

    
}
    
                     
}

