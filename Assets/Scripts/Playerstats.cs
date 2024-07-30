using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Playerstats : MonoBehaviour
{

    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject skillRewardFrame;
    private int maxLevel = 60;
    public int level = 1;
    float exp = 0;
    float maxExp = 0;
    public Vector3 spawnPoint;
    Humanoid humanoid;

    const float base_XP = 100;
    const float multiplier = 1.4f;
    const float exponent = 1.1f;

    int GetExpFormula(int level)
    {
        return Mathf.RoundToInt(base_XP * Mathf.Pow(level * multiplier, exponent));
    }

    public GameObject GetRewardFrame()
    {
        return skillRewardFrame;
    }

    public void LevelUp()
    {
        level += 1;
        if ((level-1) % 3 == 0)
        {
            skillRewardFrame.SetActive(true);
        }
        humanoid.MaxHealth = 100 + ((level-1)*10);
        humanoid.Damage(-humanoid.MaxHealth);
    }

    public void IncreaseExp(int increase)
    {
        exp += increase;
        if (exp >= maxExp)
        {
            exp -= maxExp;
            LevelUp();
            levelText.SetText(level.ToString());
            maxExp = GetExpFormula(level);
            IncreaseExp(0);

        }
    }

    private void Start()
    {
        maxExp = GetExpFormula(level);
        expBar.value = exp / maxExp;
        levelText.SetText(level.ToString());
        spawnPoint = transform.position;
        humanoid = gameObject.GetComponent<Humanoid>();
    }

    private void Update()
    {
        float targetValue = exp / maxExp;
        if (expBar.value != targetValue)
        {
            expBar.value = Mathf.Lerp(expBar.value, targetValue, Time.deltaTime * 5f);
        }
    }
}
