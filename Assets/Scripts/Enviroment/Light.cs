using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    [SerializeField] private Light2D light;

    private int frames = 0;

    [SerializeField] private int framesPerRandomize;

    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    private bool cour = false;

    // Update is called once per frame
    void Update()
    {
        frames++;
        if (frames % framesPerRandomize == 0)
        {
            if (cour == false)
            {
                RandomizeIntensity();
            }
        }
    }

    void RandomizeIntensity()
    {
        System.Random random = new System.Random();


        float randomValue = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
        //light.intensity = randomValue;
        if (cour == false)
        {
            StartCoroutine("Fade", randomValue);
        }
    }

    IEnumerator Fade(float intensity)
    {
        cour = true;
        float baseValue = light.intensity;
        float elapsedTime = 0f;
        float duration = 1f; 

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            light.intensity = Mathf.Lerp(baseValue, intensity, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        light.intensity = intensity;

        cour = false;
    }

}
