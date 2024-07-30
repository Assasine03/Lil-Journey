using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillReward : MonoBehaviour
{
    VFXEmitter.VFXEmitter vfx = new VFXEmitter.VFXEmitter();
    Skills skillInformation;
    GameObject top;
    GameObject middle;
    GameObject bottom;
    

    float topPositionY;
    float middlePositionY;
    float bottomPositionY;

    private bool rolling = false;
    private bool reset = true;
    private string[] lastSelected = new string[] { "", "", ""};
    private string select = "";

    GameObject[] topSkills;
    GameObject[] middleSkills;

    [SerializeField] GameObject filler;
    [SerializeField] GameObject selectionFrame;

    List<GameObject> skills;
    List<GameObject> skillsWithFiller;

    Transform playerTransform;
    void Start()
    {
        top = transform.Find("RollingFrame").Find("Top").gameObject;
        middle = transform.Find("RollingFrame").Find("Middle").gameObject;
        bottom = transform.Find("RollingFrame").Find("Bottom").gameObject;

        topPositionY = top.transform.Find("Skill0").position.y;
        middlePositionY = middle.transform.Find("Skill0").position.y;
        bottomPositionY = bottom.transform.Find("Skill0").position.y;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        skillInformation = GameObject.FindGameObjectWithTag("GameScripts").GetComponent<Skills>();

        topSkills = new GameObject[] { top.transform.Find("Skill0").gameObject, top.transform.Find("Skill1").gameObject, top.transform.Find("Skill2").gameObject };
        middleSkills = new GameObject[] { middle.transform.Find("Skill0").gameObject, middle.transform.Find("Skill1").gameObject, middle.transform.Find("Skill2").gameObject };

        skills = vfx.GetActiveSkills();
        skillsWithFiller = vfx.GetActiveSkills();
        skillsWithFiller.Add(filler);
        FilterSkills();
    }
    
    void Update()
    {
        if (gameObject.activeSelf && !rolling && reset == true)
        {
            rolling = true;
            reset = false;
            StartCoroutine(RollSkill(12, topSkills[0], middleSkills[0], 0));
            StartCoroutine(RollSkill(15, topSkills[1], middleSkills[1], 1));
            StartCoroutine(RollSkill(20, topSkills[2], middleSkills[2], 2));
        } 
        
    }

    void ChangeSkillIcon(GameObject skillFrame, Sprite icon)
    {
        skillFrame.transform.Find("SkillIcon").GetComponent<Image>().sprite = icon;
    }

    void CheckSpecificSkill(string skill, SkillController playerStats, int slot)
    {
        Dictionary<string, object> skillInfo = skillInformation.skillInformation[skill];
        float[] dmg = (float[])skillInfo["damage"];
        if (slot == 1)
        {
            if (playerStats.skillLevel1 >= dmg.Length-1)
            {
                foreach (GameObject item in skills)
                {
                    if (item.name == skill)
                    {
                        skills.Remove(item);
                        skillsWithFiller.Remove(item);

                        break;
                    }
                }
            }
        } else if (slot == 2)
        {
            if (playerStats.skillLevel2 >= dmg.Length-1)
            {
                foreach (GameObject item in skills)
                {
                    if (item.name == skill)
                    {
                        skills.Remove(item);
                        skillsWithFiller.Remove(item);

                        break;
                    }
                }
            }
        }
        else if (slot == 3)
        {
            if (playerStats.skillLevel3 >= dmg.Length-1)
            {
                foreach (GameObject item in skills)
                {
                    if (item.name == skill)
                    {
                        skills.Remove(item);
                        skillsWithFiller.Remove(item);

                        break;
                    }
                }
            }
        }
    }

    void FilterSkills()
    {
        SkillController playerStats = playerTransform.GetComponent<SkillController>();
        string skill1 = playerStats.skill1;
        string skill2 = playerStats.skill2;
        string skill3 = playerStats.skill3;
        if (skill1 != "")
        {
            CheckSpecificSkill(skill1, playerStats, 1);
        }
        if (skill2 != "")
        {
            CheckSpecificSkill(skill2, playerStats, 2);
        }
        if (skill3 != "")
        {
            CheckSpecificSkill(skill3, playerStats, 3);
        }
    }

    (GameObject, Sprite) GetRandomSkillIcon()
    {
        if (skills.Count < 3)
        {
            int rand = Random.Range(0, skillsWithFiller.Count);
            if (skillsWithFiller[rand].name == "Filler")
            {
                return (skillsWithFiller[rand], skillsWithFiller[rand].GetComponent<SpriteRenderer>().sprite);
            } else
            {
                return (skills[rand], skills[rand].GetComponent<Hit>().skillIcon);
            }
        } else
        {
            int rand = Random.Range(0, skills.Count);
            return (skills[rand], skills[rand].GetComponent<Hit>().skillIcon);
        }
    }

    Sprite GetSkillIconOfFrame(GameObject skillFrame)
    {
        return skillFrame.transform.Find("SkillIcon").GetComponent<Image>().sprite;
    }

    public void Exit()
    {
        selectionFrame.SetActive(false);
        gameObject.SetActive(false);
        topSkills[0].transform.position = new Vector2(topSkills[0].transform.position.x, topPositionY);
        topSkills[1].transform.position = new Vector2(topSkills[1].transform.position.x, topPositionY);
        topSkills[2].transform.position = new Vector2(topSkills[2].transform.position.x, topPositionY);
        middleSkills[0].transform.position = new Vector2(middleSkills[0].transform.position.x, middlePositionY);
        middleSkills[1].transform.position = new Vector2(middleSkills[1].transform.position.x, middlePositionY);
        middleSkills[2].transform.position = new Vector2(middleSkills[2].transform.position.x, middlePositionY);
        rolling = false;
        reset = true;
        skills = vfx.GetActiveSkills();
        skillsWithFiller = vfx.GetActiveSkills();
        skillsWithFiller.Add(filler);
        FilterSkills();
    }

    public void SelectSlot(int slot)
    {
        if (select != "")
        {
            SkillController playerStats = playerTransform.GetComponent<SkillController>();
            if (slot == 1)
            {
                playerStats.skillLevel1 = 0;
                playerStats.skill1 = select;
            }
            else if (slot == 2)
            {
                playerStats.skillLevel2 = 0;
                playerStats.skill2 = select;
            }
            else if (slot == 3)
            {
                playerStats.skillLevel3 = 0;
                playerStats.skill3 = select;
            }
            playerStats.ChangeSkillLevel("Skill" + slot, 0, false);
            playerStats.UpdateIcons();
            Exit();
        }
    }

    bool IsSkillMaxed(SkillController playerStats,string skill ,int slot)
    {
        Dictionary<string, object> skillInfo = skillInformation.skillInformation[skill];
        float[] dmg = (float[])skillInfo["damage"];
        if(slot == 1)
        {
            if (playerStats.skillLevel1 >= dmg.Length - 1)
            {
                return true;
            }
        }
        else if (slot == 2)
        {
            if (playerStats.skillLevel2 >= dmg.Length - 1)
            {
                return true;
            }
        }
        else if (slot == 3)
        {
            if (playerStats.skillLevel3 >= dmg.Length - 1)
            {
                return true;
            }
        }
        return false;
    }

    public void SelectSkill(int slot)
    {
        string curSkill = lastSelected[slot];
        SkillController playerStats = playerTransform.GetComponent<SkillController>();
        if (rolling == false && curSkill != "")
        {
            
            if (curSkill != playerStats.skill1 && curSkill != playerStats.skill2 && curSkill != playerStats.skill3)
            {
                ChangeSkillIcon(selectionFrame.transform.Find("Selected").gameObject, GetSkillIconOfFrame(topSkills[slot]));
                selectionFrame.SetActive(true);
                select = lastSelected[slot];
                
            } else
            {
                if (playerStats.skill1 == curSkill)
                {
                    playerStats.skillLevel1 += 1;
                    playerStats.ChangeSkillLevel("Skill1", playerStats.skillLevel1, IsSkillMaxed(playerStats, curSkill, 1));
                } else if (playerStats.skill2 == curSkill)
                {
                    playerStats.skillLevel2 += 1;
                    playerStats.ChangeSkillLevel("Skill2", playerStats.skillLevel2, IsSkillMaxed(playerStats, curSkill, 2));
                }
                else if(playerStats.skill3 == curSkill)
                {
                    playerStats.skillLevel3 += 1;
                    playerStats.ChangeSkillLevel("Skill3", playerStats.skillLevel3, IsSkillMaxed(playerStats, curSkill, 3));
                }
                
                Exit();
            }
            
        }
    }

    IEnumerator RollSkill(int rounds ,GameObject moving1, GameObject moving2, int slot)
    {
        
        float animationSpeed = 0.3f;
        Vector2 initPosition1 = moving1.transform.position;
        Vector2 initPosition2 = moving2.transform.position;
        int co = 0;
        while (co < rounds || rolling == false)
        {
            float timer = 0f;
            
            float goalPos1 = middlePositionY;
            float goalPos2 = bottomPositionY;
            moving1.transform.position = initPosition1;
            moving2.transform.position = initPosition2;
            (GameObject, Sprite) skillInfo = GetRandomSkillIcon();
            ChangeSkillIcon(moving1, skillInfo.Item2);
            lastSelected[slot] = skillInfo.Item1.name;
            while (timer < animationSpeed)
            {
                float scaling1 = Mathf.Lerp(initPosition1.y, goalPos1, timer / animationSpeed);
                float scaling2 = Mathf.Lerp(initPosition2.y, goalPos2, timer / animationSpeed);
                moving1.transform.position = new Vector2(initPosition1.x, scaling1);
                moving2.transform.position = new Vector2(initPosition2.x, scaling2);
                timer += Time.deltaTime;
                yield return null;
            }
            moving1.transform.position = new Vector2(initPosition1.x, goalPos1);
            moving2.transform.position = new Vector2(initPosition2.x, goalPos2);
            ChangeSkillIcon(moving2, GetSkillIconOfFrame(moving1));


            co++;
        }
        if (slot == 2)
        {
            rolling = false;
        }

    }
}
