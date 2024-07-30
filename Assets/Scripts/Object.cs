using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Object : MonoBehaviour, IPickupable
{
    [SerializeField] float pickUpDistance = 1f;
    
    public float orbitSpeed = 4f; // Speed of orbiting
    public float drag = 3f; // Drag to slow down the orbit
    private const float destroyDistance = 0.1f;
    public const float fade_time = 0.1f;

    [HideInInspector]
    public Transform playerTransform;
    [HideInInspector]
    public Playerstats playerStats;
    [HideInInspector]
    public Vector3 velocity = Vector3.zero;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = playerTransform.GetComponent<Playerstats>();
    }

    private void Update()
    {
        FakeUpdate();
        if (IsWithinInteractDistance())
        {
            Grab();
        }
    }

    public abstract void Grab();
    public abstract void FakeUpdate();
    private bool IsWithinInteractDistance()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < pickUpDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Quaternion LookAtTarget(Vector2 rotation)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    public bool IsWithinDestroyDistance()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) < destroyDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
