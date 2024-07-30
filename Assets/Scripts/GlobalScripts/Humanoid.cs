using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Humanoid : MonoBehaviour
{
    [SerializeField] public float MaxHealth = 100;
    [SerializeField] Slider HealthBar;
    Image background;
    [SerializeField] GameObject damageHitUI;
    [SerializeField] bool selfHealing = false;

    public float HealMultiplier = 1f;
    public float Health;
    float lerpSpeed = 2f;

    bool isPlayer;
    [HideInInspector]
    public bool dead = false;
    void Start()
    {
        Health = MaxHealth;
        background = HealthBar.transform.Find("Background").GetComponent<Image>();
                       
        if (gameObject.GetComponent<Enemy>())
        {
            isPlayer = false;
        } else if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
        UpdateHealthBar();
    }

    GameObject GetDebrisOfCharacter(Transform entity)
    {
        Transform debris = entity.Find("Character").Find("Debris");

        return debris.gameObject;
    }

    public void Damage(float dmg)
    {
        Health -= dmg;
        UpdateHealthBar();

        GameObject dmgUI = Instantiate(damageHitUI, GetDebrisOfCharacter(transform).transform);
        DamageHit hitDamage = dmgUI.GetComponent<DamageHit>();
        hitDamage.damage = dmg;
    }

    Color originalColor = new Color(0.514151f, 1f, 0.5178093f, 1f);
    void UpdateHealthBar()
    {
        float targetValue = Health / MaxHealth;

        if (Health >= MaxHealth)
        {
           StartCoroutine(FadeOut());
           
        }
        else if (HealthBar.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color != originalColor)
        {
           StartCoroutine(FadeIn());
           

            
        }
        HealthBar.value = Mathf.Lerp(HealthBar.value, targetValue, Time.deltaTime * lerpSpeed);
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Transform fill = HealthBar.transform.Find("Fill Area").Find("Fill");
        Color healthBarColor = fill.GetComponent<Image>().color;
        Color backgroundColor = background.color;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            fill.GetComponent<Image>().color = Color.Lerp(healthBarColor, Color.clear, elapsedTime);
            background.color = Color.Lerp(backgroundColor, Color.clear, elapsedTime);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Transform fill = HealthBar.transform.Find("Fill Area").Find("Fill");
        Color healthBarColor = originalColor;
        Color backgroundColor = new Color(1f, 1f, 1f, 1f);

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            fill.GetComponent<Image>().color = Color.Lerp(fill.GetComponent<Image>().color, healthBarColor, elapsedTime);
            background.color = Color.Lerp(background.color, backgroundColor, elapsedTime);
            yield return null;
        }
    }

    bool healingCD = false;
    IEnumerator Healing()
    {
        healingCD = true;
        Damage(-(1 * HealMultiplier));
        yield return new WaitForSeconds(1);
        healingCD = false;

    }

    public void Died(float respawnTime = 1f)
    {
        if (isPlayer == false)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            StartCoroutine(Respawn(enemy.respawnTime));
            enemy.DropReward();
            
        } else
        {
            StartCoroutine(Respawn(respawnTime));
        }
    }

    private IEnumerator Respawn(float time)
    {
        //Cast Death Animation
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        transform.Find("Character").gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.Find("Character").Find("HealthbarCanvas").GetComponent<Canvas>().enabled = false;
        yield return new WaitForSeconds(time);
        //Cast Spawn Animation
        Health = MaxHealth;
        UpdateHealthBar();
        dead = false;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        transform.Find("Character").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        transform.Find("Character").Find("HealthbarCanvas").GetComponent<Canvas>().enabled = true;
        if (isPlayer)
        {
            transform.position = gameObject.GetComponent<Playerstats>().spawnPoint;
        }
    }

    void Update()
    {
        UpdateHealthBar();
        if (!dead)
        {
            if (healingCD == false & Health < MaxHealth & selfHealing == true)
            {
                StartCoroutine(Healing());
            }
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            else if (Health <= 0)
            {
                dead = true;
                Health = 0;
                Died();
            }
        }
    }
}
