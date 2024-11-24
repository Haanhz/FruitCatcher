using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{
    //skill1: bắn ra 1 luồng gió làm chệch hướng quả
    public GameObject windPrefab;       // Prefab của gió
    public Transform spawnPoint;       // Vị trí spawn gió (nên là một điểm cố định gần người phá)
    public float windLifetime = 2f;    // Thời gian tồn tại của gió
    public float windCooldown = 1f;    // Thời gian cooldown giữa mỗi lần thổi
    private float nextWindTime = 0f;

    void Update() {
        // Kiểm tra phím Space và cooldown
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextWindTime) {
            SpawnWind();
            nextWindTime = Time.time + windCooldown; // Set thời gian cooldown
        }
    }

    void SpawnWind() {
        // Tạo gió tại vị trí spawnPoint
        GameObject wind = Instantiate(windPrefab, spawnPoint.position, Quaternion.identity);

        // Tự động hủy sau một khoảng thời gian
        Destroy(wind, windLifetime);
    }

}
