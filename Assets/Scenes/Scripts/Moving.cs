using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float baseSpeed = 5f;              // Normal speed
    public float rainSpeedMultiplier = 0.5f;    // Speed multiplier during heavy rain (e.g., 0.5 for half-speed)
    public bool loose = false;
    private float currentSpeed;
    public WeatherEffects weatherEffects;
    
    void Start()
    {
        currentSpeed = baseSpeed * Time.deltaTime;               // Set the initial speed
    }
    // Update is called once per frame
    void Update()
    {
        if (loose == false)
        {
            // Check if heavy rain effect is active and adjust speed
            if (weatherEffects != null && weatherEffects.IsHeavyRainActive())
            {
                currentSpeed = baseSpeed * rainSpeedMultiplier * Time.deltaTime;  // Slow down during heavy rain
            }
            else
            {
                currentSpeed = baseSpeed * Time.deltaTime;                        // Normal speed otherwise
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(currentSpeed, 0, 0);
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-currentSpeed, 0, 0);
            }

        }
    }
}
