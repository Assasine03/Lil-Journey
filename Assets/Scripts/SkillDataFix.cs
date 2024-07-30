using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SkillDataFix : MonoBehaviour
{


    Enemy target;

    private void Start()
    {
        target = gameObject.GetComponent<Enemy>();
    }

    void Update()
    {
        if (target)
        {
            if (target.skillData.skill.Count > target.skillData.level.Count)
            {
                target.skillData.level.Add(0);
            }
            if (target.skillData.skill.Count > target.skillData.castDuration.Count)
            {
                target.skillData.castDuration.Add(0);
            }
        }
    }
}
