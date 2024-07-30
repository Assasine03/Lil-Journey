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
if a particle has inf duration such as looped
give it back and do not destroy
 
 
 */

namespace ParticleEmitter
{
    public class ParticleEmitter : MonoBehaviour
    {

        [SerializeField] GameObject SglobalParticles;
        [SerializeField] GameObject SplayerParticles;

        public static GameObject globalParticles;
        public static GameObject playerParticles;
        
        public static Dictionary<string, GameObject> particles = new Dictionary<string, GameObject>();

        private string particleFolderPath = "Particles";

        private void Awake()
        {
            HandleParticles();
            globalParticles = SglobalParticles;
            playerParticles = SplayerParticles;
            
        }

        void HandleParticles()
        {
            GameObject[] particlePrefabs = Resources.LoadAll<GameObject>(particleFolderPath);
            foreach (GameObject particlePrefab in particlePrefabs)
            {
                string particleName = particlePrefab.name;
                particles.Add(particleName, particlePrefab);
                Debug.Log($"particle prefab {particleName} loaded.");
            }
        }

        public void GlobalParticleEmit(string key, Vector3 position)
        {
            if (particles[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                ParticleSystem particle = par.Emit(particles[key],position, globalParticles);
                
                Destroy(particle.gameObject, particle.duration+1);
            } else
            {
                Debug.LogError("Trying to cast a none loaded particle: "+ key);
            }
        }

        public void DependentParticleEmit(string key, GameObject dependency)
        {
            if (particles[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                ParticleSystem particle = par.Emit(particles[key], dependency);
                Destroy(particle.gameObject, particle.duration+1);
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded particle: " + key);
            }
        }

        public void PlayerParticleEmit(string key)
        {
            if (particles[key] != null)
            {
                EmitSystem.EmitSystem par = new EmitSystem.EmitSystem();
                ParticleSystem particle = par.Emit(particles[key], playerParticles);
                Destroy(particle.gameObject, particle.duration+1);
            }
            else
            {
                Debug.LogError("Trying to cast a none loaded particle: " + key);
            }
        }

    }
}