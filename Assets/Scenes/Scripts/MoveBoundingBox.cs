using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class RightHandController : MonoBehaviour
{
    UdpClient udpClient;
    IPEndPoint remoteEndPoint;

    // Giới hạn di chuyển trong thế giới
    public Vector2 worldBoundsMin = new Vector2(-5, -5); // Góc dưới trái
    public Vector2 worldBoundsMax = new Vector2(5, 5);   // Góc trên phải

    void Start()
    {
        udpClient = new UdpClient(65432); // Port giống với Python
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    void Update()
    {
        if (udpClient.Available > 0)
        {
            // Nhận dữ liệu từ Python
            byte[] data = udpClient.Receive(ref remoteEndPoint);
            string json = Encoding.UTF8.GetString(data);
            BoundingBox bbox = JsonUtility.FromJson<BoundingBox>(json);

            // Tính trung tâm bounding box
            float centerX = (bbox.x_min + bbox.x_max) / 2f;
            float centerY = (bbox.y_min + bbox.y_max) / 2f;

            // Scale vị trí từ màn hình sang world space
            float normalizedX = centerX / Screen.width;  // [0, 1]
            float normalizedY = centerY / Screen.height; // [0, 1]

            // Map sang tọa độ thế giới
            float worldX = Mathf.Lerp(worldBoundsMin.x, worldBoundsMax.x, normalizedX);
            float worldY = Mathf.Lerp(worldBoundsMin.y, worldBoundsMax.y, 1 - normalizedY); // Y ngược

            // Cập nhật vị trí GameObject
            transform.position = new Vector3(worldX, worldY, 0); // Z = 0 cho game 2D
        }
    }

    void OnDestroy()
    {
        udpClient.Close();
    }

    [System.Serializable]
    public class BoundingBox
    {
        public int x_min;
        public int y_min;
        public int x_max;
        public int y_max;
    }
}
