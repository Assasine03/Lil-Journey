using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkillGift : Object
{

    private bool pickedUp = false;
    private bool destroyed = false;

    [SerializeField] GameObject reward;
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
            reward = playerStats.GetRewardFrame();
            
            if (reward.activeSelf == false)
            {
                reward.SetActive(true);
                destroyed = true;
                StartCoroutine(FadeOutAndDestroy());
            }
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        SpriteRenderer spriteRenderer = transform.GetComponent<SpriteRenderer>();

        Color startColor = spriteRenderer.color;
        float startTime = Time.time;

        while (Time.time < startTime + fade_time)
        {
            float t = (Time.time - startTime) / fade_time;
            spriteRenderer.color = Color.Lerp(startColor, Color.clear, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    
}
