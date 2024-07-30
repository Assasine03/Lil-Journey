using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


namespace EmitSystem
{
    public class EmitSystem : MonoBehaviour
    {



        private ParticleSystem ps;
        private GameObject vfx;
        private bool cour = false;

        public ParticleSystem Emit(GameObject particlePrefab, GameObject instancer)
        {
            ps = particlePrefab.GetComponent<ParticleSystem>();
            ps = Instantiate(ps, instancer.transform);
            return ps;
        }

        public ParticleSystem Emit(GameObject particlePrefab, Vector3 position, GameObject instancer)
        {
            ps = particlePrefab.GetComponent<ParticleSystem>();

            ps = Instantiate(ps, position, Quaternion.identity, instancer.transform);

            //ps.gameObject.transform.position = position;
            return ps;
        }

        public GameObject VFXEmit(GameObject vfxPrefab, GameObject instancer, GameObject caster = null)
        {
            //vfx = vfxPrefab.GetComponent<GameObject>();
            vfx = Instantiate(vfxPrefab, instancer.transform);
            if (caster != null)
            {
                Hit vfxHit = vfx.GetComponent<Hit>();
                vfxHit.caster = caster;

            }
            return vfx;
        }

        public GameObject VFXEmit(GameObject vfxPrefab, Vector3 position, GameObject instancer, GameObject caster)
        {
            vfx = Instantiate(vfxPrefab, position, Quaternion.identity, instancer.transform);
            if (caster != null)
            {
                Hit vfxHit = vfx.GetComponent<Hit>();
                vfxHit.caster = caster;

            }
            return vfx;
        }

        public GameObject VFXEmit(GameObject vfxPrefab, GameObject instancer, GameObject caster, Dictionary<string, object> skillInformation)
        {
            Hit prefab = vfxPrefab.GetComponent<Hit>();
            if (prefab != null)
            {
                prefab.enabled = false;
            }
            //vfx = vfxPrefab.GetComponent<GameObject>();
            vfx = Instantiate(vfxPrefab, instancer.transform);
            Hit hitVFX = vfx.GetComponent<Hit>();
            
            if (hitVFX != null)
            {

                int[] placeHolderInt;
                float[] placeHolderFloat;
                string[] placeHolderString;
                placeHolderInt = (int[])skillInformation["maxHits"];
                hitVFX.MaxHits = placeHolderInt[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["hitDelay"];
                hitVFX.HitDelayOnCast = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["hitInterval"];
                hitVFX.HitInterval = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["damage"];
                hitVFX.Damage = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderString = (string[])skillInformation["hitVFX"];
                hitVFX.HitVFXName = placeHolderString[(int)skillInformation["level"]];
                hitVFX.enabled = true;

                if (hitVFX.directionType != (Hit.Edirection)skillInformation["direction"] && hitVFX.directionType != Hit.Edirection.center)
                {
                    Transform hitTransform = hitVFX.transform;
                    hitTransform.localScale = new Vector3(-hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                }
                else
                if (hitVFX.directionType == Hit.Edirection.random)
                {
                    Transform hitTransform = hitVFX.transform;
                    if (Random.Range(0, 1) == 1)
                    {
                        hitTransform.localScale = new Vector3(-hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                    }
                    else
                    {
                        hitTransform.localScale = new Vector3(hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                    }
                }
                if (hitVFX.directionTypeUp != (Hit.EdirectionUp)skillInformation["directionUp"] && hitVFX.directionTypeUp != Hit.EdirectionUp.center)
                {
                    Transform hitTransform = hitVFX.transform;
                    hitTransform.localScale = new Vector3(hitTransform.localScale.x, -hitTransform.localScale.y, hitTransform.localScale.z);
                }
            }
            if (caster != null)
            {
                Hit vfxHit = vfx.GetComponent<Hit>();
                vfxHit.caster = caster;

            }
            if (prefab != null)
            {
                prefab.enabled = true;
            }
            return vfx;
        }

        public GameObject VFXEmit(GameObject vfxPrefab, Vector3 position, GameObject instancer, GameObject caster, Dictionary<string, object> skillInformation)
        {
            Hit prefab = vfxPrefab.GetComponent<Hit>();
            if (prefab != null)
            {
                prefab.enabled = false;
            }
            vfx = Instantiate(vfxPrefab, position, Quaternion.identity, instancer.transform);
            Hit hitVFX = vfx.GetComponent<Hit>();
            
            if (hitVFX != null)
            {

                int[] placeHolderInt;
                float[] placeHolderFloat;
                string[] placeHolderString;
                placeHolderInt = (int[])skillInformation["maxHits"];
                hitVFX.MaxHits = placeHolderInt[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["hitDelay"];
                hitVFX.HitDelayOnCast = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["hitInterval"];
                hitVFX.HitInterval = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderFloat = (float[])skillInformation["damage"];
                hitVFX.Damage = placeHolderFloat[(int)skillInformation["level"]];

                placeHolderString = (string[])skillInformation["hitVFX"];
                hitVFX.HitVFXName = placeHolderString[(int)skillInformation["level"]];
                hitVFX.enabled = true;

                if (hitVFX.directionType != (Hit.Edirection)skillInformation["direction"] && hitVFX.directionType != Hit.Edirection.center)
                {
                    Transform hitTransform = hitVFX.transform;
                    hitTransform.localScale = new Vector3(-hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                } else 
                if (hitVFX.directionType == Hit.Edirection.random)
                {
                    Transform hitTransform = hitVFX.transform;
                    if (Random.Range(0, 1) == 1)
                    {
                        hitTransform.localScale = new Vector3(-hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                    } else
                    {
                        hitTransform.localScale = new Vector3(hitTransform.localScale.x, hitTransform.localScale.y, hitTransform.localScale.z);
                    }
                }
                if (hitVFX.directionTypeUp != (Hit.EdirectionUp)skillInformation["directionUp"] && hitVFX.directionTypeUp != Hit.EdirectionUp.center)
                {
                    Transform hitTransform = hitVFX.transform;
                    hitTransform.localScale = new Vector3(hitTransform.localScale.x, -hitTransform.localScale.y, hitTransform.localScale.z);
                }
            }
            if (caster != null)
            {
                Hit vfxHit = vfx.GetComponent<Hit>();
                vfxHit.caster = caster;

            }
            if (prefab != null)
            {
                prefab.enabled = true;
            }
            return vfx;
        }
    }

}