using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class HitBoundingBox : MonoBehaviour
{
    public GameObject windPrefab;       // Prefab của gió
    public Transform spawnPoint;       // Vị trí spawn gió
    public float windLifetime = 2f;    // Thời gian tồn tại của gió
    public float windCooldown = 1f;    // Thời gian cooldown giữa mỗi lần thổi

    private float nextWindTime = 0f;
    private bool handPreviouslyClosed = true; // Trạng thái ban đầu: Tay nắm

    private UdpClient udpClient;
    private IPEndPoint remoteEndPoint;

    void Start() {
        // Khởi tạo UDP client
        udpClient = new UdpClient(65433); // Cùng port với Python
        remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
    }

    void Update() {
        // Nhận dữ liệu từ Python
        if (udpClient.Available > 0) {
            byte[] data = udpClient.Receive(ref remoteEndPoint);
            string json = Encoding.UTF8.GetString(data);
            HandData handData = JsonUtility.FromJson<HandData>(json);

            // Kiểm tra trạng thái tay
            if (handData.hand_open) {
                // Nếu tay mở và trước đó là nắm, bắn gió
                if (handPreviouslyClosed && Time.time >= nextWindTime) {
                    SpawnWind();
                    nextWindTime = Time.time + windCooldown; // Set thời gian cooldown
                }
                // Cập nhật trạng thái: tay đang mở
                handPreviouslyClosed = false;
            } else {
                // Cập nhật trạng thái: tay đang nắm
                handPreviouslyClosed = true;
            }
        }
    }

    void SpawnWind() {
        // Tạo gió tại vị trí spawnPoint
        GameObject wind = Instantiate(windPrefab, spawnPoint.position, Quaternion.identity);

        // Tự động hủy sau một khoảng thời gian
        Destroy(wind, windLifetime);
    }

    void OnDestroy() {
        // Đóng kết nối khi không còn dùng
        udpClient.Close();
    }

    [System.Serializable]
    public class HandData {
        public int x_min;
        public int y_min;
        public int x_max;
        public int y_max;
        public bool hand_open; // True: Tay mở
    }
}
