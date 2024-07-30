using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ExpOrb : Object
{

    private bool pickedUp = false;
    private bool destroyed = false;

    public int expAmount = 100;


    Playerstats playerstats;
    public override void Grab()
    {
        
        if (!pickedUp)
        {
            pickedUp = true;
        }
    }

    public override void FakeUpdate()
    {
        if (pickedUp & destroyed == false)
        {
            Vector3 directionToTarget = playerTransform.position - transform.position;

            velocity *= (1 - drag * Time.deltaTime);

            velocity += directionToTarget.normalized * orbitSpeed * Time.deltaTime;

            transform.position += velocity * Time.deltaTime;
        }
        if (destroyed == false & pickedUp & IsWithinDestroyDistance())
        {
            destroyed = true;
            playerstats = playerTransform.GetComponent<Playerstats>();
            playerstats.IncreaseExp(expAmount);
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        Transform childTransform = transform.Find("Glow");
        SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();
        Light2D light2D = childTransform.GetComponent<Light2D>();

        Color startColor = spriteRenderer.color;
        float startintensity = light2D.intensity;
        float startTime = Time.time;

        while (Time.time < startTime + fade_time)
        {
            float t = (Time.time - startTime) / fade_time;
            spriteRenderer.color = Color.Lerp(startColor, Color.clear, t);
            light2D.intensity = Mathf.Lerp(startintensity, 0, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
