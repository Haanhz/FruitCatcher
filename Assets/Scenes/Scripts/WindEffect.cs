using UnityEngine;

public class WindEffect : MonoBehaviour {
    public float moveSpeed = 5f;        // Tốc độ di chuyển của luồng gió
    public float windStrength = 10f;   // Độ mạnh của gió

    void Update() {
        // Di chuyển luồng gió sang bên trái (trục X âm)
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}

//     private void OnCollisionEnter2D(Collision2D other) {
//             Rigidbody rb = other.GetComponent<Rigidbody>();
//             if (rb != null) {
//                 // Thêm lực đẩy lên quả theo chiều của gió (trục X)
//                 Vector3 windForce = Vector3.left * windStrength;
//                 rb.AddForce(windForce);
            
//         }
//     }
// }
