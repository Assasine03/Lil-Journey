using UnityEngine;
using System.Collections;


public class AnimationEventHandler : MonoBehaviour
{
    public void Recolor(int oppacity)
    {
        SpriteRenderer x = GetComponent<SpriteRenderer>();
        Material mat = x.material;
        Debug.Log(x.color);
    }
}