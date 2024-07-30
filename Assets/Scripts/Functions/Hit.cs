using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hit : MonoBehaviour
{

    [HideInInspector] public int MaxHits = 1;
    [HideInInspector] public float HitDelayOnCast = 0.1f;
    [HideInInspector] public float HitInterval = 0.1f;
    [HideInInspector] public float Damage = 0f;
    [HideInInspector] public string HitVFXName;
    [HideInInspector] public bool selfHit = false;
    [HideInInspector] public GameObject caster;

    [SerializeField] public Sprite skillIcon;
    [SerializeField] public Sprite skillOnCooldownIcon;
    [SerializeField] public bool RewardInChest = true;

    public enum Edirection
    {
        center,
        left,
        right,
        random
    }

    public enum EdirectionUp
    {
        center,
        up,
        down
    }

    [SerializeField] public Edirection directionType;

    [SerializeField] public EdirectionUp directionTypeUp;

    //int hits = 0;
    VFXEmitter.VFXEmitter vfx = new VFXEmitter.VFXEmitter();
    PolygonCollider2D collider;

    GameObject GetDebrisOfCharacter(Transform entity)
    {
        Transform debris = entity.Find("Character").Find("Debris");
        
        return debris.gameObject;
    }

    Humanoid GetHumanoidOfCharacter(Transform entity)
    {
        Humanoid humanoid = entity.GetComponent<Humanoid>();
        return humanoid;
    }

    void Start()
    {
        collider = gameObject.GetComponent<PolygonCollider2D>();
        collider.isTrigger = true;
    }

    bool AnimatorHasAnimation(Animator animator, string animationName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;

        if (controller != null)
        {
            for (int i = 0; i < controller.animationClips.Length; i++)
            {
                if (controller.animationClips[i].name == animationName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        //if (hits < MaxHits)
        //{
            if (selfHit == false & collide.transform.parent & collide.transform.parent.name == "Entities" & caster.name != collide.name)
            {
                Animator anim = collide.transform.Find("Character").gameObject.GetComponent<Animator>();
                StartCoroutine(HitLogic(collide, anim));
            }
            else if (selfHit == true & collide.transform.parent & collide.transform.parent.name == "Entities" & caster.name == collide.name)
            {
                Animator anim = collide.transform.Find("Character").gameObject.GetComponent<Animator>();
                StartCoroutine(HitLogic(collide, anim));
            }
       // }
    }

    void ActiveHit(Collider2D collide, Animator anim)
    {
        GameObject debris = GetDebrisOfCharacter(collide.transform);
        vfx.DependentVFXEmit(HitVFXName, debris);
        
        if (AnimatorHasAnimation(anim, collide.name + "Hit"))
        {
            anim.Play(collide.name + "Hit");
        }
        
        GetHumanoidOfCharacter(collide.transform).Damage(Damage);
    }


    private List<GameObject> hitCollection = new List<GameObject>();
    private List<int> hits = new List<int>();
    private IEnumerator HitLogic(Collider2D collide, Animator animator)
    {
        float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
        float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        float remainingTime = stateLength * (1.0f - normalizedTime);

        yield return new WaitForSeconds(HitDelayOnCast);
        int hitID = -1;
        int prevHits = 0;
        for (int i = 0; i < hitCollection.Count; i++)
        {
            if (hitCollection[i] == collide.gameObject && hits[i] < MaxHits)
            {
                hitID = i; // previous hits found more allowed
                prevHits = hits[i];
                break;
            } else if (hitCollection[i] == collide.gameObject && hits[i] >= MaxHits)
            {
                hitID = -2; // previous hits found no more allowed
                break;
            }
        }
        if (hitID == -1) // no previous hits found
        {
            hitCollection.Add(collide.gameObject);
            hits.Add(prevHits);
            hitID = hits.Count-1;
        }

        if (hitID > -1)
        {
            for (int i = 0; i < MaxHits; i++)
            {
                if (hits[hitID] >= MaxHits)
                {
                    break;
                }
                else
                {
                    hits[hitID]++;
                    ActiveHit(collide, animator); //fix if hit doesnt exist
                    yield return new WaitForSeconds(HitInterval);
                }
            }
        }

    }

}
