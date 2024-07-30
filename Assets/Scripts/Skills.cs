using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    ParticleEmitter.ParticleEmitter part = new ParticleEmitter.ParticleEmitter();
    VFXEmitter.VFXEmitter vfx = new VFXEmitter.VFXEmitter();
    string lookDirection;

    [SerializeField]
    public Dictionary<string, Dictionary<string, object>> skillInformation = new Dictionary<string, Dictionary<string, object>>();
    
    public void AddSkillInformation(string skillName, float[] damage, int[] maxHits, float[] hitDelay, float[] hitInterval, string[] hitVFX, string skillType, float cooldown, float range, bool playerBound, float offset, float positionOffset)
    {
        Dictionary<string, object> skillProperties = new Dictionary<string, object>();
        skillProperties.Add("skillName", skillName);
        skillProperties.Add("damage", damage);
        skillProperties.Add("maxHits", maxHits);
        skillProperties.Add("hitDelay", hitDelay);
        skillProperties.Add("hitInterval", hitInterval);
        skillProperties.Add("hitVFX", hitVFX);
        skillProperties.Add("skillType", skillType);
        skillProperties.Add("cooldown", cooldown);
        skillProperties.Add("useRange", range);
        skillProperties.Add("playerBound", playerBound);
        skillProperties.Add("offset", offset);
        skillProperties.Add("positionOffset", positionOffset);

        skillInformation.Add(skillName, skillProperties);
    }


    private void Awake()
    {
        #region SkillInformation
        AddSkillInformation(
            "ShadowSlash", // Skill name
            new float[] {2,4,4,7,12}, // damage each level
            new int[] { 3, 3, 3, 3, 3 }, // max Hits each level
            new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f }, // hit delay each level
            new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f }, // hit interval each level
            new string[] { "SlashHit", "SlashHit", "SlashHit", "SlashHit", "SlashHit" }, // hit vfx each level
            "base", // skill type
            1, // cooldown
            2.5f, // cast range
            true, // player bound
            0f, // offset
            0f // offset for y position
             );
        AddSkillInformation(
            "FirePillar", // Skill name
            new float[] { 4, 4, 4, 7, 12 }, // damage each level
            new int[] { 3, 6, 6, 6, 12 }, // max Hits each level
            new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f }, // hit delay each level
            new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.075f }, // hit interval each level
            new string[] { "FireHit", "FireHit", "FireHit", "FireHit", "FireHit" }, // hit vfx each level
            "base", // skill type
            5f, // cooldown
            2.5f, // cast range
            false, // player bound
            1f, // offset
            0.4f // offset for y position
             );

        AddSkillInformation(
            "HealingCast", // Skill name
            new float[] { -4, -4, -4, -7, -12 }, // damage each level
            new int[] { 3, 6, 6, 6, 12 }, // max Hits each level
            new float[] { 0f, 0f, 0f, 0f, 0f }, // hit delay each level
            new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.075f }, // hit interval each level
            new string[] { "HealingHit", "HealingHit", "HealingHit", "HealingHit", "HealingHit" }, // hit vfx each level
            "base", // skill type
            5, // cooldown
            2.5f, // cast range
            false, // player bound
            0.5f, // offset
            0.3f // offset for y position
             );

        AddSkillInformation(
            "LightningStrike", // Skill name
            new float[] { 10, 10, 16, 16, 16 }, // damage each level
            new int[] { 1, 1, 1, 1, 2 }, // max Hits each level
            new float[] { 0f, 0f, 0f, 0f, 0f }, // hit delay each level
            new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.075f }, // hit interval each level
            new string[] { "LightningHit", "LightningHit", "LightningHit", "LightningHit", "LightningHit" }, // hit vfx each level
            "base", // skill type
            8f, // cooldown
            3.5f, // cast range
            false, // player bound
            1.5f, // offset
            0.2f // offset for y position
             );

        AddSkillInformation(
            "CrowAttack", // Skill name
            new float[] { 10, 10, 16, 16, 12 }, // damage each level
            new int[] { 1, 1, 1, 1, 3 }, // max Hits each level
            new float[] { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f }, // hit delay each level
            new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.075f }, // hit interval each level
            new string[] { "SlashHit", "SlashHit", "SlashHit", "SlashHit", "SlashHit" }, // hit vfx each level
            "base", // skill type
            3, // cooldown
            3.5f, // cast range
            false, // player bound
            0.75f, // offset
            0f // offset for y position
             );

        AddSkillInformation(
            "EarthHammer", // Skill name
            new float[] { 10, 12, 16, 14, 12 }, // damage each level
            new int[] { 1, 1, 1, 1, 3 }, // max Hits each level
            new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f }, // hit delay each level
            new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.075f }, // hit interval each level
            new string[] { "EarthHit", "EarthHit", "EarthHit", "EarthHit", "EarthHit" }, // hit vfx each level
            "base", // skill type
            8, // cooldown
            2f, // cast range
            false, // player bound
            1f, // offset
            0.5f // offset for y position
             );

        AddSkillInformation(
            "EarthImpale", // Skill name
            new float[] { 3, 4, 7, 8, 12 }, // damage each level
            new int[] { 6, 6, 6, 6, 9 }, // max Hits each level
            new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }, // hit delay each level
            new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.1f }, // hit interval each level
            new string[] { "EarthHit", "EarthHit", "EarthHit", "EarthHit", "EarthHit" }, // hit vfx each level
            "base", // skill type
            9, // cooldown
            2f, // cast range
            false, // player bound
            1.5f, // offset
            0.4f // offset for y position
             );

        AddSkillInformation(
            "ScytheAttack", // Skill name
            new float[] { 3, 4, 7, 8, 9 }, // damage each level
            new int[] { 3, 3, 3, 5, 9 }, // max Hits each level
            new float[] { 0.25f, 0.25f, 0.25f, 0.25f, 0.25f }, // hit delay each level
            new float[] { 0.075f, 0.075f, 0.075f, 0.075f, 0.03f }, // hit interval each level
            new string[] { "PurpleHit", "PurpleHit", "PurpleHit", "PurpleHit", "PurpleHit" }, // hit vfx each level
            "base", // skill type
            9, // cooldown
            2f, // cast range
            false, // player bound
            0.5f, // offset
            0f // offset for y position
             );

        AddSkillInformation(
            "WindStrike", // Skill name
            new float[] { 4, 5, 6, 4, 5 }, // damage each level
            new int[] { 2, 2, 2, 3, 5 }, // max Hits each level
            new float[] { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f }, // hit delay each level
            new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.075f }, // hit interval each level
            new string[] { "WindHit", "WindHit", "WindHit", "WindHit", "WindHit" }, // hit vfx each level
            "base", // skill type
            5f, // cooldown
            2.5f, // cast range
            false, // player bound
            1f, // offset
            0f // offset for y position
             );

        AddSkillInformation(
            "Windup", // Skill name
            new float[] { 1, 1, 2, 2, 2 }, // damage each level
            new int[] { 8, 12, 12, 16, 24 }, // max Hits each level
            new float[] { 0.2f, 0.2f, 0.2f, 0.2f, 0.2f }, // hit delay each level
            new float[] { 0.106f, 0.06f, 0.06f, 0.046f, 0.03f }, // hit interval each level
            new string[] { "WindHit", "WindHit", "WindHit", "WindHit", "WindHit" }, // hit vfx each level
            "base", // skill type
            8f, // cooldown
            3.5f, // cast range
            false, // player bound
            1f, // offset
            0f // offset for y position
             );
        #endregion
    }
    string GetDirectionName()
    {
        return lookDirection;
    }

    float AnimLenght(Animator animator)
    {
        float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float remainingTime = stateLength * (1.0f - normalizedTime);
        return remainingTime;
    }

    public string GetCurrentAnimationName(Animator animator)
    {
        if (animator != null && animator.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
        else
        {
            return "";
        }
    }

    

    Animator GetAnimatorFromEntity(GameObject obj)
    {
        return obj.transform.Find("Character").GetComponent<Animator>();
    }

    public Dictionary<string, object> SkillCast(GameObject caster, string skill, int level, Vector3 position, Vector3 directionOffset = new Vector3())
    {
        StartCoroutine(cast(caster, skill, level, position, directionOffset));
        return skillInformation[skill];
    }

    private IEnumerator cast(GameObject caster, string skill, int level)
    {
        Dictionary<string, object> skillInfo = new Dictionary<string, object>(skillInformation[skill]);
        skillInfo.Add("level", level);
        skillInfo.Add("caster", caster);
        skillInfo.Add("target", new Vector3());
        skillInfo.Add("directionOffset", new Vector3());
        skillInfo.Add("direction", Hit.Edirection.center);
        skillInfo.Add("directionUp", Hit.EdirectionUp.center);
        yield return StartCoroutine(skill+level, skillInfo);
        
        
    }

    private IEnumerator cast(GameObject caster, string skill, int level, Vector3 position, Vector3 directionOffset)
    {
        Dictionary<string, object> skillInfo = new Dictionary<string, object>(skillInformation[skill]);
        skillInfo.Add("level", level);
        skillInfo.Add("caster", caster);
        skillInfo.Add("target", position);
        skillInfo.Add("directionOffset", directionOffset);
        Vector3 directionCheck = position + directionOffset;
        if (caster.transform.position.x > directionCheck.x)
        {
            skillInfo.Add("direction", Hit.Edirection.left);
        } else
        {
            skillInfo.Add("direction", Hit.Edirection.right);
        }
        if (caster.transform.position.y > directionCheck.y)
        {
            skillInfo.Add("directionUp", Hit.EdirectionUp.down);
        }
        else
        {
            skillInfo.Add("directionUp", Hit.EdirectionUp.up);
        }
        try // Normal Cast
        {
            StartCoroutine(skill + level, skillInfo);
        }
        catch // Cast if skill exists in a none levelable manner
        {
            StartCoroutine(skill, skillInfo);
        }
        yield return null;
    }

    #region Skills
    #region ShadowSlash
    private IEnumerator ShadowSlash0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        part.PlayerParticleEmit("FlashStepShadow");
        anim.Play("CastTryFurther");
        StartCoroutine(Slash(caster, 0.3f, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ShadowSlash1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        part.PlayerParticleEmit("FlashStepShadow");
        anim.Play("CastTryFurther");
        StartCoroutine(Slash(caster, 0.3f, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ShadowSlash2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        part.PlayerParticleEmit("FlashStepShadow");
        anim.Play("CastTryFurther");
        StartCoroutine(Slash(caster, 0.3f, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ShadowSlash3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        part.PlayerParticleEmit("FlashStepShadow");
        anim.Play("CastTryFurther");
        StartCoroutine(Slash(caster, 0.3f, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ShadowSlash4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        part.PlayerParticleEmit("FlashStepShadow");
        anim.Play("CastTryFurther");
        StartCoroutine(Slash(caster, 0.3f, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    
    private IEnumerator Slash(GameObject caster, float t, Dictionary<string, object> skillInfo)
    {
        yield return new WaitForSeconds(t);
        Vector3 pos = new Vector3(0, -0.5f, 0);
        vfx.GlobalVFXEmit(skillInfo, caster.transform.position + pos, caster);
    }
    #endregion

    #region FirePillar
    private IEnumerator FirePillar0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator FirePillar1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator FirePillar2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator FirePillar3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator FirePillar4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    #endregion

    #region HealingCast
    private IEnumerator HealingCast0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator HealingCast1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator HealingCast2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator HealingCast3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator HealingCast4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    #endregion

    #region LightningStrike
    private IEnumerator LightningStrike0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        StartCoroutine(Lightning(caster, 0, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator LightningStrike1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        StartCoroutine(Lightning(caster, 1, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator LightningStrike2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        StartCoroutine(Lightning(caster, 2, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator LightningStrike3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        StartCoroutine(Lightning(caster, 3, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator LightningStrike4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        StartCoroutine(Lightning(caster, 4, skillInfo));
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator Lightning(GameObject caster, int lvl, Dictionary<string, object> skillInfo)
    {
        Vector3 pos = (Vector3)skillInfo["target"];
        Vector3 directionOffset = (Vector3)skillInfo["directionOffset"];
        vfx.GlobalVFXEmit(skillInfo, directionOffset + pos, caster);
        if (lvl > 0)
        {
            yield return new WaitForSeconds(0.3f);
            Vector3 oldPos = directionOffset;
            directionOffset = directionOffset + new Vector3(-0.6f * oldPos.x, -0.6f * oldPos.y, 0);
            vfx.GlobalVFXEmit(skillInfo, pos + directionOffset, caster);
        }
        if (lvl > 2)
        {
            yield return new WaitForSeconds(0.3f);
            Vector3 oldPos2 = directionOffset;
            directionOffset = directionOffset + new Vector3(0.6f * oldPos2.x, 0.6f * oldPos2.y, 0);
            vfx.GlobalVFXEmit(skillInfo, pos + directionOffset, caster);
        }
    }
    #endregion

    #region CrowAttack
    private IEnumerator CrowAttack0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        CrowAttack(caster, skillInfo);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator CrowAttack1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        CrowAttack(caster, skillInfo);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator CrowAttack2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        CrowAttack(caster, skillInfo);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator CrowAttack3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        CrowAttack(caster, skillInfo);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator CrowAttack4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        CrowAttack(caster, skillInfo);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    void CrowAttack(GameObject caster, Dictionary<string, object> skillInfo)
    {
       vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
    }
    #endregion

    #region EarthHammer
    private IEnumerator EarthHammer0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthHammer1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthHammer2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthHammer3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator EarthHammer4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    #endregion

    #region EarthImpale
    private IEnumerator EarthImpale0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthImpale1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthImpale2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthImpale3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator EarthImpale4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    #endregion

    #region ScytheAttack
    private IEnumerator ScytheAttack0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ScytheAttack1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ScytheAttack2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ScytheAttack3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator ScytheAttack4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    #endregion

    #region WindStrike
    private IEnumerator WindStrike0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator WindStrike1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator WindStrike2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator WindStrike3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    private IEnumerator WindStrike4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    #endregion

    #region Windup
    private IEnumerator Windup0(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator Windup1(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator Windup2(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator Windup3(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }
    private IEnumerator Windup4(Dictionary<string, object> skillInfo)
    {
        GameObject caster = skillInfo["caster"] as GameObject;
        Animator anim = GetAnimatorFromEntity(caster);
        anim.Play(GetDirectionName() + "Cast");
        vfx.GlobalVFXEmit(skillInfo, (Vector3)skillInfo["target"] + (Vector3)skillInfo["directionOffset"], caster);
        yield return new WaitForSeconds(AnimLenght(anim));
    }

    #endregion
    #endregion
}
