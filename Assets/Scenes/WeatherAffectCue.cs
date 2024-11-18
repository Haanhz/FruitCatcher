using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeatherAffectCue : MonoBehaviour
{
    [SerializeField] public GameObject laser;
    public float initialDelay = 30.0f;   // Delay for the first appearance
    public float visibleDuration = 3.0f; // Duration to keep laser visible
    public float interval = 60.0f;       // Interval between activations
    private WeatherEffects weatherEffects;

    void Start()
    {
        // Find the WeatherEffects script in the scene
        weatherEffects = FindObjectOfType<WeatherEffects>();
        laser.SetActive(false);
        InvokeRepeating("ActivateLaser", initialDelay, interval); // Start after 30s and repeat every interval
    }

    void ActivateLaser()
    {
        laser.SetActive(true);                   // Show the laser
        Invoke("DeactivateLaser", visibleDuration); // Schedule to hide laser after visibleDuration
    }

    void DeactivateLaser()
    {
        laser.SetActive(false);                  // Hide the laser
        // Use Invoke to activate a weather effect after the laser turns off
        if (weatherEffects != null)
        {
            weatherEffects.Invoke("ActivateWeatherEffect", 0f); // Immediately call ActivateWeatherEffect
        }
    }
}
