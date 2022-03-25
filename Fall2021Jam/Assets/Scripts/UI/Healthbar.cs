using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    protected Image healthbarFill;
    public Entity entity;

    protected virtual void Start()
    {
        healthbarFill = GetComponent<Image>();
        entity = GetComponentInParent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbarFill.fillAmount = entity.GetHealth() / entity.GetMaxHealth();
    }
}
