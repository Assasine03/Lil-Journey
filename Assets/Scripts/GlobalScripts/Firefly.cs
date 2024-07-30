using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Unity.VisualScripting;

public class Fireflies : MonoBehaviour
{
    private GameObject mainFolder;
    private Vector2 mainSize = new Vector2(200, 200);
    private List<GameObject> fireflies = new List<GameObject>();
    private Camera mainCamera;
    private GameObject oldestFirefly;
    private float cameraViewThreshold = 0f; // Adjust this value for a smaller camera view threshold

    [SerializeField] GameObject debrisFolder;

    void Start()
    {
        mainCamera = Camera.main;

        mainFolder = new GameObject("fireflies");
        mainFolder.transform.parent = debrisFolder.transform;

        StartCoroutine(SpawnFireflies());
    }

    private Vector2 CamOrientate()
    {
        return mainCamera.transform.position + new Vector3(
            Random.Range(-mainSize.x / 2, mainSize.x / 2),
            Random.Range(-mainSize.y / 2, mainSize.y / 2),
            0
        );
    }

    private float Magnitude(Vector2 position)
    {
        return position.magnitude;
    }

    private IEnumerator SpawnFireflies()
    {
        while (true)
        {
            Vector2 cameraPosition = mainCamera.transform.position;
            int firefliesInView = 0;

            for (int i = 0; i < fireflies.Count; i++)
            {
                if (Magnitude(mainSize) * 1.1f <= Magnitude((Vector2)fireflies[i].transform.position - cameraPosition))
                {
                    StartCoroutine(FadeOutAndDestroy(fireflies[i]));
                    if (fireflies[i] == oldestFirefly)
                    {
                        oldestFirefly = null;
                    }
                    fireflies.RemoveAt(i);
                    i--; // Decrement the index as we removed an item
                }
                else
                {
                    firefliesInView++;
                }
            }

            if (firefliesInView >= 80 && oldestFirefly == null)
            {
                oldestFirefly = FindOldestFirefly(cameraPosition);
            }

            if (firefliesInView >= 80 && oldestFirefly != null)
            {
                StartCoroutine(FadeOutAndDestroy(oldestFirefly));
                fireflies.Remove(oldestFirefly);
                oldestFirefly = null;
            }

            if (fireflies.Count < 80)
            {
                GameObject firefly = Instantiate(Resources.Load<GameObject>("fireflyPrefab"), CamOrientate(), Quaternion.identity);
                firefly.transform.parent = mainFolder.transform;
                fireflies.Add(firefly);
            }

            yield return new WaitForSeconds(1);
        }
    }

    private GameObject FindOldestFirefly(Vector2 cameraPosition)
    {
        GameObject oldest = null;
        float oldestDistance = float.MaxValue;

        foreach (GameObject firefly in fireflies)
        {
            float distance = Magnitude((Vector2)firefly.transform.position - cameraPosition);
            if (distance < oldestDistance)
            {
                oldestDistance = distance;
                oldest = firefly;
            }
        }

        return oldest;
    }

    private IEnumerator FadeOutAndDestroy(GameObject firefly)
    {
        Transform childTransform = firefly.transform.Find("firefly"); 
        SpriteRenderer spriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        Transform childTransform2 = firefly.transform.Find("fireflyLight");
        Light2D light2D = childTransform2.GetComponent<Light2D>();

        Color startColor = spriteRenderer.color;
        float startintensity = light2D.intensity;
        float startTime = Time.time;

        while (Time.time < startTime + 0.5f)
        {
            float t = (Time.time - startTime) / 0.5f;
            spriteRenderer.color = Color.Lerp(startColor, Color.clear, t);
            light2D.intensity = Mathf.Lerp(startintensity, 0, t);
            yield return null;
        }

        Destroy(firefly);
    }

    void Update()
    {
        for (int i = 0; i < fireflies.Count; i++)
        {
            Vector2 targetPosition = CamOrientate();
            float distance = Vector2.Distance(fireflies[i].transform.position, targetPosition);

            if (distance > 0.1f) // Adjust this threshold to suit your needs
            {
                float speed = 0.05f; // Adjust the speed of movement
                float interpolationFactor = speed * Time.deltaTime;
                fireflies[i].transform.position = Vector2.Lerp(fireflies[i].transform.position, targetPosition, interpolationFactor);
            }
            else
            {
                fireflies[i].transform.position = targetPosition;
            }
        }
    }
}
