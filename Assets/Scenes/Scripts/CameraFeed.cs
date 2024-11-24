using UnityEngine;

public class CameraFeed : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    void Start()
    {
        // Tạo đối tượng webcamTexture
        webcamTexture = new WebCamTexture();

        // Gắn webcamTexture vào material của Quad
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;

        // Bắt đầu phát video từ camera
        webcamTexture.Play();
    }
}
