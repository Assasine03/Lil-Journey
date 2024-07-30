using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageHit : MonoBehaviour
{
    [SerializeField] public float damage;
    
    private TextMeshPro tmp;
    public float scaleDuration = 0.5f; 
    public float fadeDuration = 0.2f;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
        //tmp.SetText(damage.ToString());

        
        StartCoroutine(Dmg());
    }





    private IEnumerator Dmg()
    {
        // Wait until damage is set
        while (damage == 0)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (damage > 0)
        {
            tmp.color = new Color(1f, 0.5123727f, 0.3915094f, 1f);
        } else if (damage < 0)
        {
            tmp.color = new Color(0.2495225f, 1f, 0.1650943f, 1f);
            damage = -damage;
        }
        
        tmp.SetText(damage.ToString());
        RectTransform rectTransform = tmp.GetComponent<RectTransform>();
        Vector3 newPos = new Vector2(Random.Range(-1f, 1f), Random.Range(0f, 1f));
        rectTransform.anchoredPosition = newPos;


        Vector3 initialScale = Vector3.one * 0.2f;
        Vector3 targetScale = Vector3.one * 0.05f;


        float scaleStartTime = Time.time;
        while (Time.time < scaleStartTime + scaleDuration)
        {
            float t = (Time.time - scaleStartTime) / scaleDuration;
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }



        Color startColor = tmp.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        float fadeStartTime = Time.time;
        while (Time.time < fadeStartTime + fadeDuration)
        {
            float t = (Time.time - fadeStartTime) / fadeDuration;
            tmp.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
