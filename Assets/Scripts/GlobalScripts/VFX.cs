using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UIElements;
using System;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.InputSystem;

/*
 IMPORTANT: only waste your time if needed
if a vfx has inf duration such as looped
give it back and do not destroy
 
 
 */

namespace VFXEmitter
{
    public class VFXEmitter : MonoBehaviour
    {

        [SerializeField] GameObject SglobalVFX;
        [SerializeField] GameObject SplayerVFX;

        public static GameObject globalVFX;
        public static GameObject playerVFX;

        public static Dictionary<string, GameObject> VFX = new Dictionary<string, GameObject>();

        private string vfxFolderPath = "VFX"; // Set the correct relative path here

        private void Awake()
        {
            HandleVFX();
            globalVFX = SglobalVFX;
            playerVFX = SplayerVFX;
        }

        void HandleVFX()
        {
            GameObject[] vfxPrefabs = Resources.LoadAll<GameObject>(vfxFolderPath);
            foreach (GameObject vfxPrefab in vfxPrefabs)
            {
                string vfxName = vfxPrefab.name;
                VFX.Add(vfxName, vfxPrefab);
                Debug.Log($"VFX prefab {vfxName} loaded.");
            }
        }

        private Vector2 GetColliderSize(PolygonCollider2D polygonCollider)
        {
            Vector2 minPoint = polygonCollider.points[0];
            Vector2 maxPoint = polygonCollider.points[0];

            foreach (Vector2 point in polygonCollider.points)
            {
                if (point.x < minPoint.x)
                    minPoint.x = point.x;
                if (point.y < minPoint.y)
                    minPoint.y = point.y;
                if (point.x > maxPoint.x)
                    maxPoint.x = point.x;
                if (point.y > maxPoint.y)
                    maxPoint.y = point.y;
            }

            float width = maxPoint.x - minPoint.x;
            float height = maxPoint.y - minPoint.y;

            return new Vector2(width, height);
        }

        public Sprite GetVFXIcon(string skill)
        {
            return VFX[skill].GetComponent<Hit>().skillIcon;
        }

        public Sprite GetVFXCDIcon(string skill)
        {
            return VFX[skill].GetComponent<Hit>().skillOnCooldownIcon;
        }

        public PolygonCollider2D GetVFXCollider(string skill)
        {
            return VFX[skill].GetComponent<PolygonCollider2D>();
        }

        public List<GameObject> GetActiveSkills()
        {
            List<GameObject> activeSkills = new List<GameObject>();
            foreach (KeyValuePair<string, GameObject> pair in VFX)
            {
                Hit hitComp = pair.Value.GetComponent<Hit>();
                if (hitComp != null)
                {
                    if (hitComp.RewardInChest)
                    {
                        activeSkills.Add(pair.Value);
                    }
                }
            }
            return activeSkills;
        }

        public GameObject HitIndicatorEmit(string skill, Vector3 position, float duration)
        {
            EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
            GameObject indicator = par.VFXEmit(VFX["HitIndicator"], globalVFX);
            
            AttackIndicator attIndicator = indicator.GetComponent<AttackIndicator>();
            attIndicator.SetScale(GetColliderSize(GetVFXCollider(skill)));
            attIndicator.timeForAttack = duration;
            attIndicator.startAnimation = true;
            indicator.transform.position = position;
            return indicator;
        }


        public void GlobalVFXEmit(string key, Vector3 position, GameObject caster = null)
        {
            if (VFX[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();

                GameObject vfxEffect = par.VFXEmit(VFX[key], position, globalVFX, caster);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    Transform visualTry = vfxEffect.transform.Find(key + "Visual");
                    if (visualTry != null)
                    {
                        animator = visualTry.GetComponent<Animator>();
                    }
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key);
            }
        }

        public void DependentVFXEmit(string key, GameObject dependency, GameObject caster = null)
        {
            if (VFX[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                GameObject vfxEffect = par.VFXEmit(VFX[key], dependency, caster);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    Transform visualTry = vfxEffect.transform.Find(key + "Visual");
                    if (visualTry != null)
                    {
                        animator = visualTry.GetComponent<Animator>();
                    }
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key);
            }
        }

        public void PlayerVFXEmit(string key, GameObject caster = null)
        {
            if (VFX[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                GameObject vfxEffect = par.VFXEmit(VFX[key], playerVFX, caster);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    Transform visualTry = vfxEffect.transform.Find(key + "Visual");
                    if (visualTry != null)
                    {
                        animator = visualTry.GetComponent<Animator>();
                    }
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key);
            }
        }

        public void GlobalVFXEmit(Dictionary<string, object> key, Vector3 position, GameObject caster = null)
        {
            if (VFX[key["skillName"] as string] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();

                GameObject vfxEffect = par.VFXEmit(VFX[key["skillName"] as string], position, globalVFX, caster, key);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    animator = vfxEffect.transform.Find(key["skillName"] as string + "Visual").GetComponent<Animator>();
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key["skillName"] as string);
            }
        }

        public void DependentVFXEmit(Dictionary<string, object> key, GameObject dependency, GameObject caster = null)
        {
            if (VFX[key["skillName"] as string] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                GameObject vfxEffect = par.VFXEmit(VFX[key["skillName"] as string], dependency, caster, key);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    animator = vfxEffect.transform.Find(key["skillName"] as string + "Visual").GetComponent<Animator>();
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key["skillName"] as string);
            }
        }

        public void PlayerVFXEmit(Dictionary<string, object> key, GameObject caster = null)
        {
            if (VFX[key["skillName"] as string] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                GameObject vfxEffect = par.VFXEmit(VFX[key["skillName"] as string], playerVFX, caster, key);

                Animator animator = vfxEffect.GetComponent<Animator>();
                if (animator == null)
                {
                    animator = vfxEffect.transform.Find(key["skillName"] as string + "Visual").GetComponent<Animator>();
                }
                if (animator != null)
                {
                    float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
                    float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    float remainingTime = stateLength * (1.0f - normalizedTime);

                    Destroy(vfxEffect.gameObject, remainingTime);
                }
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded vfx: " + key["skillName"] as string);
            }
        }

    }
}