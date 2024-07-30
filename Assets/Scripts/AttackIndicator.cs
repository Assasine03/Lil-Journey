using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;


public class AttackIndicator : MonoBehaviour
{
    public Transform shape;
    public Transform growEffect;
    private float size = 1f;
    public float timeForAttack = 1f;
    public bool startAnimation = false;

    private float fade_time = 0.1f;

    private void Update()
    {
        if (startAnimation)
        {
            StartCoroutine(ScaleOverTime());
            startAnimation = false; // Reset startAnimation to prevent coroutine from running multiple times
        }
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = new Vector3(scale.x * 1.5f, scale.y * 1.5f, scale.z * 1.5f);
    }

    IEnumerator ScaleOverTime()
    {
        float timer = 0f;
        Vector3 initialScale = growEffect.localScale;
        while (timer < timeForAttack)
        {
            float scaling = Mathf.Lerp(initialScale.y, size, timer / timeForAttack);
            growEffect.localScale = new Vector3(initialScale.x, scaling, initialScale.z);
            timer += Time.deltaTime;
            yield return null;
        }
        growEffect.localScale = new Vector3(initialScale.x, size, initialScale.z);
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        SpriteRenderer spriteRenderer = shape.GetComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer2 = growEffect.GetComponent<SpriteRenderer>();

        Color startColor = spriteRenderer.color;
        Color startColor2 = spriteRenderer2.color;
        float startTime = Time.time;

        while (Time.time < startTime + fade_time)
        {
            float t = (Time.time - startTime) / fade_time;
            spriteRenderer.color = Color.Lerp(startColor, Color.clear, t);
            spriteRenderer2.color = Color.Lerp(startColor2, Color.clear, t);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}




