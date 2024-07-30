using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SkillController : MonoBehaviour
{
    VFXEmitter.VFXEmitter vfx = new VFXEmitter.VFXEmitter();

    private bool cd = false;
    private float cdTime = 1;

    [SerializeField] private Skills skill;
    [SerializeField] GameObject skillFrame;


    public string baseSkill = "ShadowSlash";
    public string skill1 = "";
    public int skillLevel1 = 0;
    public string skill2 = "";
    public int skillLevel2 = 0;
    public string skill3 = "";
    public int skillLevel3 = 0;

    public bool skill1CD = false;
    public bool skill2CD = false;
    public bool skill3CD = false;
    string lookDirection;
    void ChangeSkillIcon(string skill, Sprite icon)
    {
        skillFrame.transform.Find(skill).Find("SkillIcon").GetComponent<Image>().sprite = icon;
    }

    public void ChangeSkillLevel(string skill, int level, bool maxed)
    {
        string lvl = "";
        if (maxed == true)
        {
            lvl = "MAX";
        }
        else if (level <= 0)
        {
            lvl = "";
        }
        else if (level < 4)
        {
            for (int i = 0; i < level; i++)
            {
                lvl += "I";
            }
        }
        skillFrame.transform.Find(skill).Find("Level").GetComponent<TextMeshProUGUI>().SetText(lvl);
    }

    void ChangeSkillTimer(string skill, float time)
    {
        if (time > 0.01f)
        {
            skillFrame.transform.Find(skill).Find("Cooldown").GetComponent<TextMeshProUGUI>().SetText(time.ToString("F1"));
        } else
        {
            skillFrame.transform.Find(skill).Find("Cooldown").GetComponent<TextMeshProUGUI>().SetText("");
        }
    }

    public void UpdateIcons()
    {
        if (!cd && baseSkill != "")
        {
            ChangeSkillIcon("Skill" + 0, vfx.GetVFXIcon(baseSkill));
        }
        if (!skill1CD && skill1 != "")
        {
            ChangeSkillIcon("Skill" + 1, vfx.GetVFXIcon(skill1));
        }
        if (!skill2CD && skill2 != "")
        {
            ChangeSkillIcon("Skill" + 2, vfx.GetVFXIcon(skill2));
        }
        if (!skill3CD && skill3 != "")
        {
            ChangeSkillIcon("Skill" + 3, vfx.GetVFXIcon(skill3));
        }
    }

    private void Start()
    {
        if (baseSkill != "")
        {
            ChangeSkillIcon("Skill0" ,vfx.GetVFXIcon(baseSkill));
        }
        if (skill1 != "")
        {
            ChangeSkillIcon("Skill1", vfx.GetVFXIcon(skill1));
        }
        if (skill2 != "")
        {
            ChangeSkillIcon("Skill2", vfx.GetVFXIcon(skill2));
        }
        if (skill3 != "")
        {
            ChangeSkillIcon("Skill3", vfx.GetVFXIcon(skill3));
        }
    }
    void Update()
    {
        lookDirection = gameObject.GetComponent<Walking>().lookDirection;
        if (Time.timeScale != 0)
        {
            
            if (cd == false) {
                if (Input.GetKeyDown(KeyCode.X) && !skill1CD && skill1 != "")
                {
                    CastSkill(skill1, skillLevel1, 1);
                }
                if (Input.GetKeyDown(KeyCode.C) && !skill2CD && skill2 != "")
                {
                    CastSkill(skill2, skillLevel2, 2);
                }
                if (Input.GetKeyDown(KeyCode.V) && !skill3CD && skill3 != "")
                {
                    CastSkill(skill3, skillLevel3, 3);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    CastSkill(baseSkill,0, 0);
                }
            }
        }
    }

    void CastSkill(string skillName, int level = 0, int slot = 0)
    {
        Dictionary<string, object> skillInformation = skill.skillInformation[skillName];

        Vector3 position = transform.position + new Vector3(0, (float)skillInformation["positionOffset"], 0);
        Vector3 directionOffset = GetDirectionReturn((float)skillInformation["offset"]);
        Dictionary<string, object> skillInfo = skill.SkillCast(gameObject, skillName, level, position, directionOffset);
        StartCoroutine(castCooldown(skillInfo, slot));
        StartCoroutine(generalCooldown());
    }

    Vector3 GetDirectionReturn(float distance)
    {
        if (lookDirection == "front")
        {
            return new Vector3(0, -distance, 0);
        }
        else if (lookDirection == "back")
        {
            return new Vector3(0, distance, 0);
        }
        else if (lookDirection == "left")
        {
            return new Vector3(-distance, 0, 0);
        }
        else if (lookDirection == "right")
        {
            return new Vector3(distance, 0, 0);
        }
        return new Vector3();
    }

    private IEnumerator castCooldown(Dictionary<string, object> skillInfo, int slot)
    {
        float cooldown = (float)skillInfo["cooldown"];

        ChangeSkillIcon("Skill"+slot, vfx.GetVFXCDIcon(skillInfo["skillName"] as string));
        if (slot == 1)
        {
            skill1CD = true;
        }
        else if (slot == 2)
        {
            skill2CD = true;
        }
        else if (slot == 3)
        {
            skill3CD = true;
        }
        float remainingTime = cooldown;
        float decrementAmount = 0.1f;
        while (remainingTime > 0f)
        {
            
            remainingTime -= decrementAmount;
            ChangeSkillTimer("Skill" + slot, remainingTime);
            yield return new WaitForSeconds(decrementAmount);
        }

        if (slot == 1)
        {
            skill1CD = false;
        }
        else if (slot == 2)
        {
            skill2CD = false;
        }
        else if (slot == 3)
        {
            skill3CD = false;
        }
        ChangeSkillIcon("Skill" + slot, vfx.GetVFXIcon(skillInfo["skillName"] as string));
    }

    private IEnumerator generalCooldown()
    {
        cd = true;

        yield return new WaitForSeconds(cdTime);
        cd = false;
    }




}
