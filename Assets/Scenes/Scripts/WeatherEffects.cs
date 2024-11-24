using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WeatherEffects : MonoBehaviour
{
    [SerializeField] public GameObject misty;
    [SerializeField] public GameObject mistyEffect;
    [SerializeField] public GameObject hurricane;
    [SerializeField] public GameObject heavyrain;
    public float weatherEffectDuration = 5.0f;

    void Start()
    {
        misty.SetActive(false);
        hurricane.SetActive(false);
        heavyrain.SetActive(false);
    }

    public bool IsHeavyRainActive()
    {
        return heavyrain.activeSelf;
    }

    public void ActivateWeatherEffect()
    {
        // Choose a random weather effect and activate it
        GameObject chosenEffect = ChooseRandomWeatherEffect();
        if (chosenEffect != null)
        {
            chosenEffect.SetActive(true);                 // Activate chosen effect
            Invoke("DeactivateWeatherEffect", weatherEffectDuration); // Schedule deactivation
        }
    }

    void Update()
    {
        if(misty.activeSelf)
        {
            mistyEffect.SetActive(true);
        }
        else
        {
            mistyEffect.SetActive(false);
        }
    }

    void DeactivateWeatherEffect()
    {
        // Deactivate all weather effects (you can customize this if needed)
        misty.SetActive(false);
        hurricane.SetActive(false);
        heavyrain.SetActive(false);
    }

    GameObject ChooseRandomWeatherEffect()
    {
        GameObject[] weatherEffects = { misty, hurricane, heavyrain };
        int randomIndex = Random.Range(0, weatherEffects.Length);
        return weatherEffects[randomIndex];
    }
}
