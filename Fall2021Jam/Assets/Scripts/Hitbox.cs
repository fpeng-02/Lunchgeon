using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float damage;
    [SerializeField] private float knock;
    [SerializeField] private string targetTag;

    public float getDamage()
    {
        return damage;
    }
    public float getDuration()
    {
        return duration;
    }
    public float getKnock()
    {
        return knock;
    }
    public string getTargetTag()
    {
        return targetTag;
    }
    public void setDuration(float duration)
    {
        this.duration = duration;
    }
}
