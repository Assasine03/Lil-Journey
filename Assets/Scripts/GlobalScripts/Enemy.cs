using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SkillData
{
    public List<string> skill;
    public List<int> level;
    public List<float> castDuration;
}

public class Enemy : MonoBehaviour
{
    VFXEmitter.VFXEmitter vfx = new VFXEmitter.VFXEmitter();
    

    [HideInInspector]
    Transform playerTransform;
    [HideInInspector]
    List<bool> cooldowns = new List<bool>();
    
    private Skills skill;
    enum EenemyType
    {
        Enemey,
        Boss
    }

    [SerializeField] EenemyType enemyType;
    [SerializeField] bool canMove;
    [SerializeField] float speed = 1f;
    [SerializeField] public float respawnTime = 10f;
    [SerializeField] public float expDrop = 5f;
    [SerializeField] public float giftDrop_chance = 3f;
    [SerializeField] public SkillData skillData;

    private bool cd = false;
    Humanoid humanoid;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        foreach (var item in skillData.skill)
        {
            cooldowns.Add(false);
        }
        skill = GameObject.FindGameObjectWithTag("GameScripts").transform.GetComponent<Skills>();
        humanoid = gameObject.GetComponent<Humanoid>();
    }

    private void Update()
    {
        if (cd == false && humanoid.dead == false)
        {
            int co = 0;
            foreach (string item in skillData.skill)
            {
                Dictionary<string, object> skillInformation = skill.skillInformation[item];
                if (skillInformation != null)
                {
                    if (cooldowns[co] == false && IsWithinDistance((float)skillInformation["useRange"]) && cd == false)
                    {
                        if (IsAlive(playerTransform))
                        {
                            StartCoroutine(CastSkill(skill, item, skillData.level[co], co));
                        }
                    }
                }
                co++;
            }
        }
        
    }

    private IEnumerator CastSkill(Skills skilll, string skill, int level = 0, int skillSlot = 0)
    {
        
        Vector3 castPosition = playerTransform.position;
        float castDur = skillData.castDuration[skillSlot];
        if (castDur > 0)
        {
            vfx.HitIndicatorEmit(skill, castPosition, castDur);
        }
        cooldowns[skillSlot] = true;
        cd = true;
        yield return new WaitForSeconds(castDur);
        Dictionary<string, object> skillInfo = skilll.SkillCast(gameObject, skill, level, castPosition);
        StartCoroutine(castCooldown(skillInfo, skillSlot));
        StartCoroutine(generalCooldown());

    }

    private IEnumerator castCooldown(Dictionary<string, object> skillInfo, int slot)
    {
        float cooldown = (float)skillInfo["cooldown"];

        cooldowns[slot] = true;
        yield return new WaitForSeconds(cooldown);
        cooldowns[slot] = false;
    }


    private IEnumerator generalCooldown()
    {
        cd = true;

        yield return new WaitForSeconds(1);
        cd = false;
    }


    private bool IsWithinDistance(float distance)
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsAlive(Transform character)
    {
        return !character.GetComponent<Humanoid>().dead;
    }

    public void DropReward()
    {
        float rand = Random.Range(0f,100f);
        if (rand < giftDrop_chance)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

            vfx.GlobalVFXEmit("WhiteSkillGift", pos);

        } else
        {
            for (int i = 0; i < Random.Range(1, expDrop); i++)
            {
                Vector3 pos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                vfx.GlobalVFXEmit("ExpOrb", pos);
            }
        }
    }
}
